

namespace KasiCornerKota_Domain.Interfaces
{
    public interface IBlobStorageService
    {
        string? GenerateSASUrl(string? blobUrl);
        Task<string> UploadToBlobAsync(Stream data, string fileName);
    }
}
