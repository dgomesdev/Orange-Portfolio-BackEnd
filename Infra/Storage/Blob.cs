using Azure.Storage.Blobs;

namespace Orange_Portfolio_BackEnd.Infra.Storage
{
    public class Blob : IBlob
    {
        private readonly IConfiguration _configuration;

        public Blob(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<string> Upload(IFormFile file)
        {
            using var stream = new MemoryStream();
            file.CopyTo(stream);
            stream.Position = 0;

            // Generate a string with the current date in the format "yyyyMMdd"
            string dateString = DateTime.Now.ToString("yyyyMMddHHmmss");

            // Generate random number between 10000 and 1000000
            Random r = new Random();
            int random = r.Next(10000, 1000001);

            // Combine all parts to form the new file name
            string newFileName = $"{random}_{dateString}_{file.FileName}";

            var container = new BlobContainerClient(_configuration["Blob:ConnectionString"], _configuration["Blob:ContainerName"]);

            // Upload the file with the new name
            await container.UploadBlobAsync(newFileName, stream);

            // Return the full URL of the file in blob storage
            return container.Uri.AbsoluteUri + "/" + newFileName;
        }
    }
}
