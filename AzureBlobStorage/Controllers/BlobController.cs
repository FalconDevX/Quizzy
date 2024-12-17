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
        public async Task<IActionResult> DownloadAllBlobs(string ContainerName)
        {
            try
            {
                string connectionString = _configuration["AzureBlob:ConnectionString"]!;
                string containerName = ContainerName;

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

        [HttpDelete("delete-blob")]
        public async Task<IActionResult> DeleteBlob(string containerName, string blobName)
        {
            try
            {
                string connectionString = _configuration["AzureBlob:ConnectionString"]!;
                BlobContainerClient containerClient = new BlobContainerClient(connectionString, containerName);

                BlobClient blobClient = containerClient.GetBlobClient(blobName);

                if (await blobClient.ExistsAsync())
                {
                    await blobClient.DeleteAsync();
                    return Ok(new { Message = $"Blob '{blobName}' has been deleted." });
                }
                else
                {
                    return NotFound(new { Message = $"Blob '{blobName}' does not exist." });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred while deleting the blob.", Details = ex.Message });
            }
        }


        [HttpPost("upload")]
        public async Task<IActionResult> UploadBlob(string containerName, IFormFile file)
        {
            try
            {
                if (file == null || file.Length == 0)
                {
                    return BadRequest(new { Message = "No file was uploaded or file is empty." });
                }

                string connectionString = _configuration["AzureBlob:ConnectionString"]!;
                BlobContainerClient containerClient = new BlobContainerClient(connectionString, containerName);

                // Ensure the container exists
                await containerClient.CreateIfNotExistsAsync();
                containerClient.SetAccessPolicy(PublicAccessType.Blob);

                BlobClient blobClient = containerClient.GetBlobClient(file.FileName);

                using (var stream = file.OpenReadStream())
                {
                    await blobClient.UploadAsync(stream, overwrite: true);
                }

                return Ok(new { Message = $"File '{file.FileName}' has been uploaded successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred while uploading the file.", Details = ex.Message });
            }
        }

        [HttpGet("download-blob")]
        public async Task<IActionResult> DownloadBlob(string containerName, string blobName)
        {
            try
            {
                // Pobierz łańcuch połączenia z konfiguracji
                string connectionString = _configuration["AzureBlob:ConnectionString"]!;
                BlobContainerClient containerClient = new BlobContainerClient(connectionString, containerName);

                // Sprawdź, czy kontener istnieje
                if (!await containerClient.ExistsAsync())
                {
                    return NotFound(new { Message = $"Container '{containerName}' does not exist." });
                }

                // Wyszukaj blob z nazwą odpowiadającą podanej (bez rozszerzenia)
                BlobItem? matchingBlob = null;

                await foreach (BlobItem blob in containerClient.GetBlobsAsync())
                {
                    string blobNameWithoutExtension = Path.GetFileNameWithoutExtension(blob.Name);
                    if (string.Equals(blobNameWithoutExtension, blobName, StringComparison.OrdinalIgnoreCase))
                    {
                        matchingBlob = blob;
                        break;
                    }
                }

                if (matchingBlob == null)
                {
                    return NotFound(new { Message = $"Blob with name '{blobName}' (any extension) in container '{containerName}' does not exist." });
                }

                BlobClient blobClient = containerClient.GetBlobClient(matchingBlob.Name);

                var blobDownloadInfo = await blobClient.DownloadAsync();

                // Zwróć plik jako odpowiedź
                return File(blobDownloadInfo.Value.Content, blobDownloadInfo.Value.ContentType, matchingBlob.Name);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred while downloading the blob.", Details = ex.Message });
            }
        }






    }
}
