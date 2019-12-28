using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FileApplication.BL.Entities;
using FileApplication.BL.Models;
using FileApplication.BL.Repositories;

namespace FileApplication.BL.Services
{
    public class FolderComponentService : IFolderComponentService
    {
        public ComponentType Type => ComponentType.Folder;

        private readonly IFolderRepository _repository;

        public FolderComponentService(IFolderRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Component>> GetAllAsync()
        {
            var items = await _repository.GetAllAsync();
            return items.Select(ToComponent);
        }

        public async Task DeleteAsync(string id)
        {
            await _repository.DeleteAsync(id);
        }

        public async Task RenameAsync(string id, string name)
        {
            await _repository.RenameAsync(id, name);
        }

        public async Task<Component> CopyToAsync(Component component, Component parent)
        {
            var folder = new Folder
            {
                Name = $"Copy of {component.Name}",
                ParentFolderId = parent.Id
            };
            
            await _repository.CreateAsync(folder);

            return ToComponent(folder);
        }

        public async Task<FolderComponent> CreateAsync(string parentId, string name)
        {
            var folder = new Folder
            {
                Name = name,
                ParentFolderId = parentId
            };

            await _repository.CreateAsync(folder);

            return ToComponent(folder);
        }
        
        private FolderComponent ToComponent(Folder file)
        {
            return new FolderComponent
            {
                Id = file.Id,
                Name = file.Name,
                ParentId = file.ParentFolderId
            };
        }
    }
}