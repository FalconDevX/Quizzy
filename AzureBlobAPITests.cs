using NUnit.Framework;
using Moq;
using Moq.Protected;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using WPF;
using System.Threading;

namespace UnitTest
{
    [TestFixture]
    public class AzureBlobAPITests
    {
        private Mock<HttpMessageHandler> _httpMessageHandlerMock;
        private AzureBlobAPI _azureBlobAPI;

        [SetUp]
        public void Setup()
        {
            _httpMessageHandlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            var httpClient = new HttpClient(_httpMessageHandlerMock.Object)
            {
                BaseAddress = new Uri("https://quizzydatastorage-fthmhzfpgngphpb4.polandcentral-01.azurewebsites.net")
            };
            _azureBlobAPI = new AzureBlobAPI();
        }

        [Test]
        public async Task CreateContainerAsync_ShouldReturnSuccessMessage_WhenContainerIsCreated()
        {
            // Arrange
            string containerName = "przykladowykont";
            string jsonResponse = $"{{ \"Message\": \"Container '{containerName}' has been created successfully.\" }}";
            string expectedResponse = $"Kontener '{containerName}' został pomyślnie utworzony.\n\n";

            _httpMessageHandlerMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.Is<HttpRequestMessage>(req =>
                        req.Method == HttpMethod.Post &&
                        req.RequestUri.AbsolutePath.Contains("/api/Container/create-container") &&
                        req.RequestUri.Query.Contains($"containerName={containerName}")),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(jsonResponse) // Symulowana odpowiedź serwera
                });

            // Act
            string result = await _azureBlobAPI.CreateContainerAsync(containerName);

            // Assert
            Assert.AreEqual(expectedResponse, result, "Method should return a success message when the container is created.");
        }


        [Test]
        public async Task CreateContainerAsync_ShouldReturnConflictMessage_WhenContainerAlreadyExists()
        {
            // Arrange
            string containerName = "nowygh";
            string expectedResponse = $"Kontener '{containerName}' już istnieje.";

            _httpMessageHandlerMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.Is<HttpRequestMessage>(req =>
                        req.Method == HttpMethod.Post &&
                        req.RequestUri.AbsolutePath.Contains("/api/Container/create-container") &&
                        req.RequestUri.Query.Contains($"containerName={containerName}")),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.Conflict
                });

            // Act
            string result = await _azureBlobAPI.CreateContainerAsync(containerName);

            // Assert
            Assert.AreEqual(expectedResponse, result, "Method should return a conflict message when the container already exists.");
        }


        [Test]
        public async Task DownloadAndExtractBlobsAsync_ShouldCallCorrectEndpoint_WhenContainerNameIsValid()
        {
            // Arrange
            string containerName = "test-container";
            _httpMessageHandlerMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.Is<HttpRequestMessage>(req =>
                        req.Method == HttpMethod.Get &&
                        req.RequestUri.AbsolutePath.Contains("/api/Blob/download-all") &&
                        req.RequestUri.Query.Contains($"containerName={containerName}")),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new ByteArrayContent(new byte[100]) // Symulujemy plik ZIP
                });

            // Act
            await _azureBlobAPI.DownloadAndExtractBlobsAsync(containerName);

            // Assert
            // Jeśli brak wyjątków, test uznany za zaliczony.
            Assert.Pass("Blobs downloaded and extracted successfully.");
        }

        [Test]
        public async Task UploadBlobToApi_ShouldSucceed_WhenFileIsUploadedSuccessfully()
        {
            // Arrange
            string filePath = "test.json";
            string containerName = "ident51";

            _httpMessageHandlerMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.Is<HttpRequestMessage>(req =>
                        req.Method == HttpMethod.Post &&
                        req.RequestUri.AbsolutePath.Contains("/api/Blob/upload") &&
                        req.RequestUri.Query.Contains($"containerName={containerName}")),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK
                });

            // Act
            Assert.DoesNotThrowAsync(async () => await _azureBlobAPI.UploadBlobToApi(filePath, containerName));
        }

        [Test]
        public async Task CheckContainerExistsByUserID_ShouldReturnTrue_WhenContainerExists()
        {
            // Arrange
            string containerName = "ident6";
            _httpMessageHandlerMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.Is<HttpRequestMessage>(req =>
                        req.Method == HttpMethod.Get &&
                        req.RequestUri.AbsolutePath.Contains("/api/Container/CheckContainerExists") &&
                        req.RequestUri.Query.Contains($"containerName={containerName}")),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK
                });

            // Act
            bool result = await _azureBlobAPI.CheckContainerExistsByUserID(containerName);

            // Assert
            Assert.IsTrue(result, "Container should exist.");
        }

        [Test]
        public async Task GetBlobImageAsync_ShouldReturnImage_WhenBlobExists()
        {
            // Arrange
            string containerName = "files6";
            string blobName = "0";

            _httpMessageHandlerMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.Is<HttpRequestMessage>(req =>
                        req.Method == HttpMethod.Get &&
                        req.RequestUri.AbsolutePath.Contains("/api/Blob/download-blob") &&
                        req.RequestUri.Query.Contains($"containerName={containerName}") &&
                        req.RequestUri.Query.Contains($"blobName={blobName}")),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new ByteArrayContent(new byte[100]) // Symulujemy dane obrazu
                });

            // Act
            var image = await _azureBlobAPI.GetBlobImageAsync(containerName, blobName);

            // Assert
            Assert.IsNotNull(image, "Image should be returned when blob exists.");
        }
    }
}
