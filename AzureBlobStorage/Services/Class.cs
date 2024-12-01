using Azure;
using Azure.Storage.Blobs;
using Azure.Storage.Sas;
using Microsoft.Extensions.Configuration;

namespace AzureBlobQuizAPI.Services
{
    public class BlobStorageService
    {
        private readonly BlobContainerClient _blobContainerClient;

        public BlobStorageService(IConfiguration configuration)
        {
            var blobServiceEndpoint = configuration["AzureBlobStorage:BlobServiceEndpoint"];
            var sasToken = configuration["AzureBlobStorage:SASToken"];

            var blobUri = new Uri($"{blobServiceEndpoint}/Quizzes");
            _blobContainerClient = new BlobContainerClient(blobUri, new AzureSasCredential(sasToken));
        }

        public async Task UploadFileAsync(string fileName, Stream fileStream)
        {
            await _blobContainerClient.CreateIfNotExistsAsync();
            var blobClient = _blobContainerClient.GetBlobClient(fileName);
            await blobClient.UploadAsync(fileStream, overwrite: true);
        }

        public async Task<Stream> DownloadFileAsync(string fileName)
        {
            var blobClient = _blobContainerClient.GetBlobClient(fileName);
            var response = await blobClient.DownloadAsync();
            return response.Value.Content;
        }

        public async Task<IEnumerable<string>> ListFilesAsync()
        {
            var blobItems = _blobContainerClient.GetBlobsAsync();
            var fileNames = new List<string>();

            await foreach (var blobItem in blobItems)
            {
                fileNames.Add(blobItem.Name);
            }

            return fileNames;
        }
    }
}
