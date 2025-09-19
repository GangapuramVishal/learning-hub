namespace BlobStorage
{
    public interface IBlobService
    {
        Task<string> UploadFileBlobAsync(string filePath, string fileName);
        Task<Stream> GetBlobAsync(string blobName);
        Task DeleteBlobAsync(string blobName);
    }

}
