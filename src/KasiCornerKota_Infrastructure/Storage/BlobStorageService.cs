using Azure.Storage.Blobs;
using Azure.Storage.Sas;
using KasiCornerKota_Domain.Interfaces;
using KasiCornerKota_Infrastructure.Configuration;
using Microsoft.Extensions.Options;

namespace KasiCornerKota_Infrastructure.Storage
{
    internal class BlobStorageService(IOptions<BlobStorageSettings> blobStorageSettingsOptions) : IBlobStorageService
    {
        private readonly BlobStorageSettings _blobStorageSettings = blobStorageSettingsOptions.Value;

        public async Task<string> UploadToBlobAsync(Stream data, string fileName)
        {
            var options = new BlobClientOptions(BlobClientOptions.ServiceVersion.V2021_10_04);
            var blobServiceClient = new BlobServiceClient(_blobStorageSettings.ConnectionString, options);

            var containerClient = blobServiceClient.GetBlobContainerClient(_blobStorageSettings.LogosContainerName);

            var blobClient = containerClient.GetBlobClient(fileName);
            await blobClient.UploadAsync(data, overwrite: true);
            var blobUrl = blobClient.Uri.ToString();
            return blobUrl;
        }

        public  string? GenerateSASUrl(string? blobUrl)
        {
            if (blobUrl is null)
            {
                return null;
            }

            var sasBuilder = new BlobSasBuilder()
            {
                BlobContainerName = _blobStorageSettings.LogosContainerName,
                Resource = "b",
                StartsOn = DateTimeOffset.UtcNow,
                ExpiresOn = DateTimeOffset.UtcNow.AddMinutes(25), 
                BlobName = GetBlobNameFromUrl(blobUrl)
            };

            sasBuilder.SetPermissions(BlobSasPermissions.Read);

            var blobServiceClient = new BlobServiceClient(_blobStorageSettings.ConnectionString);

            var sasToken = sasBuilder
                .ToSasQueryParameters(new Azure.Storage.StorageSharedKeyCredential(blobServiceClient.AccountName, _blobStorageSettings.AccountKey))
                .ToString();

            return $"{blobUrl}?{sasToken}";
        }

        private string GetBlobNameFromUrl(string blobUrl)
        {
            var uri = new Uri(blobUrl);
            return uri.Segments.Last();
        }
    }
}
