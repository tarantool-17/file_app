using System.Threading.Tasks;
using FileApplication.BL.Providers;

namespace FileApplication.Data.Providers
{
    public class FileStoreProvider : IFileStoreProvider
    {
        public Task UploadDocumentAsync(string name, object stream)
        {
            throw new System.NotImplementedException();
        }

        public Task DeleteDocumentAsync(string name)
        {
            throw new System.NotImplementedException();
        }
    }
}