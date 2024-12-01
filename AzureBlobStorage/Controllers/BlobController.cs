using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Threading.Tasks;

namespace AzureBlobAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BlobController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public BlobController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet("download-all")]
        public async Task<IActionResult> DownloadAllBlobs()
        {
            try
            {
                string connectionString = _configuration["AzureBlob:ConnectionString"];
                string containerName = _configuration["AzureBlob:ContainerName"];

                if (string.IsNullOrEmpty(connectionString) || string.IsNullOrEmpty(containerName))
                {
                    return BadRequest(new { Message = "Azure Blob Storage settings are missing or invalid." });
                }

                BlobContainerClient containerClient = new BlobContainerClient(connectionString, containerName);

                if (!await containerClient.ExistsAsync())
                {
                    return NotFound(new { Message = $"Container '{containerName}' does not exist." });
                }

                var zipStream = new MemoryStream();
                try
                {
                    using (var archive = new System.IO.Compression.ZipArchive(zipStream, System.IO.Compression.ZipArchiveMode.Create, true))
                    {
                        await foreach (BlobItem blobItem in containerClient.GetBlobsAsync())
                        {
                            BlobClient blobClient = containerClient.GetBlobClient(blobItem.Name);

                            using (var blobStream = await blobClient.OpenReadAsync())
                            {
                                var entry = archive.CreateEntry(blobItem.Name, System.IO.Compression.CompressionLevel.Fastest);

                                using (var entryStream = entry.Open())
                                {
                                    await blobStream.CopyToAsync(entryStream);
                                }
                            }
                        }
                    }

                    zipStream.Position = 0;

                    return File(zipStream, "application/zip", "all-blobs.zip");
                }
                catch
                {
                    zipStream.Dispose(); 
                    throw;
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred while downloading blobs.", Details = ex.Message });
            }
        }

        



    }
}
