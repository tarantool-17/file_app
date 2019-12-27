using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using FileApplication.BL.Entities;
using FileApplication.BL.Extensions;
using FileApplication.BL.Models;
using FileApplication.BL.Providers;
using FileApplication.BL.Repositories;

namespace FileApplication.BL.Services
{
    public interface IFileService : IItemActionService
    {
        Task UploadFileAsync(FileModel file, Stream stream);
        Task<Stream> DownloadAsync(string id);
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
        
        public async Task DeleteAsync(string id)
        {
            //TODO: Smart delete from provider.
            
            await _repository.DeleteAsync(id);
        }

        public async Task RenameAsync(string id, string name)
        {
            await _repository.RenameAsync(id, name);
        }

        public Task CopyAsync(string id, string parentId)
        {
            throw new System.NotImplementedException();
        }

        public async Task UploadFileAsync(FileModel file, Stream stream)
        {
            var src = await _provider.UploadDocumentAsync(stream);

            file.Id = src;
            
            await _repository.CreateAsync(file.ToEntity());
        }

        public async Task<Stream> DownloadAsync(string id)
        {
            return await _provider.GetDocumentStreamAsync(id);
        }
    }
}