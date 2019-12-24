using System.Threading.Tasks;

namespace FileApplication.BL.Providers
{
    public interface IFileStoreProvider
    {
        Task UploadDocumentAsync(string name, object stream);
        Task DeleteDocumentAsync(string name);
    }
}