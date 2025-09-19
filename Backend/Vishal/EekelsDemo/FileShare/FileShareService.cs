using Azure.Storage.Files.Shares;
using Azure.Storage.Files.Shares.Models;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Threading.Tasks;

namespace FileShare
{
    public class FileShareService : IFileShareService
    {
        private readonly ShareClient _shareClient;
        private readonly string _shareName;

        public FileShareService(IConfiguration configuration)
        {
            var connectionString = configuration["AzureFileShare:ConnectionString"];
            _shareName = configuration["AzureFileShare:ShareName"];
            _shareClient = new ShareClient(connectionString, _shareName);
        }

        public async Task<string> DownloadFileAsync(string fileName)
        {
            var directoryClient = _shareClient.GetRootDirectoryClient();
            var fileClient = directoryClient.GetFileClient(fileName);

            if (await fileClient.ExistsAsync())
            {
                ShareFileDownloadInfo download = await fileClient.DownloadAsync();
                using (var stream = new MemoryStream())
                {
                    await download.Content.CopyToAsync(stream);
                    return System.Text.Encoding.UTF8.GetString(stream.ToArray());
                }
            }

            return null;
        }
    }
}
