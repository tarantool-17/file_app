using System.IO;
using System.Threading.Tasks;
using FileApplication.BL.Models;
using FileApplication.BL.Services.Base;

namespace FileApplication.BL.Services
{
    public interface IFileComponentService : IComponentService
    {
        Task<FileComponent> UploadFileAsync(string parentId, string name, Stream stream);
        Task<Stream> DownloadAsync(string id);
    }
}