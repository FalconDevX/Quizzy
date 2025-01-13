using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.IO;
using System.Threading.Tasks;

namespace AzureBlobAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContainerController : Controller
    {
        private readonly IConfiguration _configuration;

        public ContainerController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost("create-container")]
        public async Task<IActionResult> CreateContainer([FromQuery] string containerName)
        {
            try
            {
                string connectionString = _configuration["AzureBlob:ConnectionString"]!;

                if (string.IsNullOrEmpty(containerName))
                {
                    return BadRequest(new { Message = "Container name cannot be null or empty." });
                }

                BlobServiceClient blobServiceClient = new BlobServiceClient(connectionString);

                BlobContainerClient containerClient = await blobServiceClient.CreateBlobContainerAsync(containerName);

                return Ok(new { Message = $"Container '{containerName}' has been created successfully." });
            }
            catch (Azure.RequestFailedException ex) when (ex.ErrorCode == "ContainerAlreadyExists")
            {
                return Conflict(new { Message = $"Container '{containerName}' already exists." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred while creating the container.", Details = ex.Message });
            }
        }



        [HttpDelete("delete-container")]
        public async Task<IActionResult> DeleteContainer([FromQuery] string containerName)
        {
            try
            {
                string connectionString = _configuration["AzureBlob:ConnectionString"]!;

                if (string.IsNullOrEmpty(containerName))
                {
                    return BadRequest(new { Message = "Container name cannot be null or empty." });
                }

                BlobServiceClient blobServiceClient = new BlobServiceClient(connectionString);

                BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(containerName);

                if (!await containerClient.ExistsAsync())
                {
                    return NotFound(new { Message = $"Container '{containerName}' does not exist." });
                }

                await containerClient.DeleteAsync();

                return Ok(new { Message = $"Container '{containerName}' has been deleted successfully." });
            }
            catch (Azure.RequestFailedException ex)
            {
                return StatusCode(500, new { Message = "An error occurred while deleting the container.", Details = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An unexpected error occurred.", Details = ex.Message });
            }
        }

        [HttpGet("CheckContainerExists")]
        public async Task<bool> CheckContainerExists([FromQuery] string containerName)
        {
            try
            {
                string connectionString = _configuration["AzureBlob:ConnectionString"]!;

                if (string.IsNullOrEmpty(containerName))
                {
                    return false; // Zwracamy false, jeśli nazwa kontenera jest pusta
                }

                BlobServiceClient blobServiceClient = new BlobServiceClient(connectionString);
                BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(containerName);

                // Sprawdzamy, czy kontener istnieje
                bool exists = await containerClient.ExistsAsync();

                return exists; // Zwracamy wartość logiczną
            }
            catch
            {
                return false; // W przypadku błędu zwracamy false
            }
        }

    }
}
