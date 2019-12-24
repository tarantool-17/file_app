using System.Threading.Tasks;
using FileApplication.BL.Entities;
using FileApplication.BL.Models;
using FileApplication.BL.Providers;
using FileApplication.BL.Repositories;

namespace FileApplication.BL.Services
{
    public interface IFileService : IItemActionService
    {
        Task UploadFileAsync(FileModel file, object stream);
        Task DownloadAsync(int id);
    }
    
    public class FileService : IFileService
    {
        private readonly IFileStoreProvider _provider;
        private readonly IFileRepository _repository;
        
        public ItemType Type => ItemType.File;

        public FileService(IFileStoreProvider provider, IFileRepository repository)
        {
            _provider = provider;
            _repository = repository;
        }
        
        public Task DeleteAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task RenameAsync(int id, string name)
        {
            throw new System.NotImplementedException();
        }

        public Task CopyAsync(int id, int? parentId)
        {
            throw new System.NotImplementedException();
        }

        public Task UploadFileAsync(FileModel file, object stream)
        {
            throw new System.NotImplementedException();
        }

        public Task DownloadAsync(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}