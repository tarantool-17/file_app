using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using FileApplication.BL.Models;
using FileApplication.BL.Providers;
using FileApplication.BL.Repositories;
using File = FileApplication.BL.Entities.File;

namespace FileApplication.BL.Services
{
    public class FileComponentService : IFileComponentService
    {
        public ComponentType Type => ComponentType.File;

        private readonly IFileStoreProvider _provider;
        private readonly IFileRepository _repository;

        public FileComponentService(IFileStoreProvider provider, IFileRepository repository)
        {
            _provider = provider;
            _repository = repository;
        }

        public async Task<IEnumerable<Component>> GetAllAsync()
        {
            var items = await _repository.GetAllAsync();
            return items.Select(ToComponent);
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

        public async Task<Component> CopyToAsync(Component component, Component parent)
        {
            var fileComponent = component as FileComponent;
            
            var file = new File
            {
                Src = fileComponent?.Src,
                Name = $"Copy of {component.Name}",
                Size = fileComponent?.Size ?? 0,
                ParentFolderId = parent.Id
            };
            
            await _repository.CreateAsync(file);

            return ToComponent(file);
        }
        
        public async Task<FileComponent> UploadFileAsync(string parentId, string name, Stream stream)
        {
            var src = await _provider.UploadDocumentAsync(stream);

            var file = new File
            {
                Src = src,
                Name = name,
                Size = stream.Length,
                ParentFolderId = parentId
            };
            
            await _repository.CreateAsync(file);

            return ToComponent(file);
        }

        public async Task<Stream> DownloadAsync(string id)
        {
            return await _provider.GetDocumentStreamAsync(id);
        }

        private FileComponent ToComponent(File file)
        {
            return new FileComponent
            {
                Id = file.Id,
                Name = file.Name,
                Size = file.Size,
                ParentId = file.ParentFolderId
            };
        }
    }
}