using System.IO;
using System.Threading.Tasks;

namespace FileApplication.BL.Providers
{
    public interface IFileStoreProvider
    {
        Task<Stream> GetDocumentStreamAsync(string src);
        Task<string> UploadDocumentAsync(Stream stream);
        Task DeleteDocumentAsync(string src);
    }
}