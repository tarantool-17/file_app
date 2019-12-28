using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using FileApplication.BL.Models;
using FileApplication.BL.Services.Base;

namespace FileApplication.BL.Services
{
    public interface IFacade
    {
        Task<Component> GetTreeAsync(bool rebuild = false);
        Task RenameAsync(ComponentType type, string id, string newName);
        Task DeleteAsync(ComponentType type, string id);
        Task CopyAsync(ComponentType type, string id);
        Task<Stream> DownloadFileAsync(string id);
        Task UploadFileAsync(string id, string name, Stream stream);
        Task CreateSubfolder(string id, string name);
    }
    
    public class Facade : IFacade
    {
        private Component _root;

        private readonly ITreeBuilder _builder;
        private readonly IFileComponentService _fileService;
        private readonly IFolderComponentService _folderService;
        private readonly IEnumerable<IComponentService> _services;

        public Facade(
            ITreeBuilder builder,
            IFileComponentService fileService,
            IFolderComponentService folderService,
            IEnumerable<IComponentService> services)
        {
            _builder = builder;
            _fileService = fileService;
            _folderService = folderService;
            _services = services;
        }

        public async Task<Component> GetTreeAsync(bool rebuild = false)
        {
            if (_root == null || rebuild)
            {
                _root = await _builder.BuildFolderTree();
            }
            
            return _root;
        }

        public async Task RenameAsync(ComponentType type, string id, string newName)
        {
            var component = _root.FindByIdAndType(type, id);
            
            var service = _services.FirstOrDefault(x => x.Type == component.Type);

            await service.RenameAsync(id, newName);

            component.Rename(newName);
        }

        public async Task DeleteAsync(ComponentType type, string id)
        {
            var component = _root.FindByIdAndType(type, id);

            await DeleteComponent(component);
        }
        
        private async Task DeleteComponent(Component component)
        {
            var service = _services.FirstOrDefault(x => x.Type == component.Type);
            
            await service.DeleteAsync(component.Id);
            
            if (component.Children != null)
            {
                foreach (var child in component.Children)
                {
                    DeleteComponent(child);
                }
            }
            
            component.Delete();
        }

        public async Task CopyAsync(ComponentType type, string id)
        {
            var component = _root.FindByIdAndType(type, id);

            await CopyComponentTo(component, component.Parent);
        }
        
        private async Task CopyComponentTo(Component component, Component parent)
        {
            var service = _services.FirstOrDefault(x => x.Type == component.Type);

            var newComponent = await service.CopyToAsync(component, parent);
            
            if (component.Children != null)
            {
                foreach (var child in component.Children)
                {
                    await CopyComponentTo(child, newComponent);
                }
            }
            
            component.CopyTo(parent);
        }

        public async Task<Stream> DownloadFileAsync(string id)
        {
            return await _fileService.DownloadAsync(id);
        }

        public async Task UploadFileAsync(string id, string name, Stream stream)
        {
            var component = _root.FindByIdAndType(ComponentType.Folder, id);
            
            var newComponent = await _fileService.UploadFileAsync(id, name, stream);
            
            component.AddChild(newComponent);
        }

        public async Task CreateSubfolder(string id, string name)
        {
            var component = _root.FindByIdAndType(ComponentType.Folder, id);

            var newComponent = await _folderService.CreateAsync(id, name);
            
            component.AddChild(newComponent);
        }
    }
}