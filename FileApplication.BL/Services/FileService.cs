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
        Task<Stream> DownloadAsync(int id);
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
        
        public async Task DeleteAsync(int id)
        {
            //TODO: Smart delete from provider.
            
            await _repository.DeleteAsync(id);
        }

        public async Task RenameAsync(int id, string name)
        {
            //TODO: Rename without get.
            
            var file = await _repository.GetAsync(id);

            if (file == null)
                throw new KeyNotFoundException();

            file.Name = name;
            
            await _repository.UpdateAsync(file);
        }

        public Task CopyAsync(int id, int? parentId)
        {
            throw new System.NotImplementedException();
        }

        public async Task UploadFileAsync(FileModel file, Stream stream)
        {
            var src = await _provider.UploadDocumentAsync(stream);

            file.Src = src;
            
            await _repository.CreateAsync(file.ToEntity());
        }

        public async Task<Stream> DownloadAsync(int id)
        {
            var file = await _repository.GetAsync(id);

            if (file == null)
                throw new KeyNotFoundException();

            return await _provider.GetDocumentStreamAsync(file.Src);
        }
    }
}