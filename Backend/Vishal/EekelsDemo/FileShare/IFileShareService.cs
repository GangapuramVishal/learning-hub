namespace FileShare
{
    public interface IFileShareService
    {
        Task<string> DownloadFileAsync(string fileName);
    }

}
