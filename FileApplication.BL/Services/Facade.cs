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
            return await _builder.GetTreeAsync();
        }

        public async Task RenameAsync(ComponentType type, string id, string newName)
        {
            var component = await GetTreeComponentAsync(type, id);
            
            var service = _services.FirstOrDefault(x => x.Type == component.Type);

            await service.RenameAsync(id, newName);

            component.Rename(newName);
        }

        public async Task DeleteAsync(ComponentType type, string id)
        {
            var component = await GetTreeComponentAsync(type, id);

            await DeleteComponent(component);
        }
        
        private async Task DeleteComponent(Component component)
        {
            var service = _services.FirstOrDefault(x => x.Type == component.Type);
            
            await service.DeleteAsync(component.Id);
            
            if (component.Children != null)
            {
                var children = component.Children.ToList();
                foreach (var child in children)
                {
                    DeleteComponent(child);
                }
            }
            
            component.Delete();
        }

        public async Task CopyAsync(ComponentType type, string id)
        {
            var component = await GetTreeComponentAsync(type, id);

            await CopyComponentTo(component, component.Parent);
        }
        
        private async Task CopyComponentTo(Component component, Component parent)
        {
            var service = _services.FirstOrDefault(x => x.Type == component.Type);

            var dbComponent = await service.CopyToAsync(component, parent);
            
            var treeComponent = component.CopyTo(parent, dbComponent.Id);
            
            if (component.Children != null)
            {
                foreach (var child in component.Children)
                {
                    await CopyComponentTo(child, treeComponent);
                }
            }
        }

        public async Task<Stream> DownloadFileAsync(string id)
        {
            return await _fileService.DownloadAsync(id);
        }

        public async Task UploadFileAsync(string id, string name, Stream stream)
        {
            var component = await GetTreeComponentAsync(ComponentType.Folder, id);
            
            var newComponent = await _fileService.UploadFileAsync(id, name, stream);
            
            component.AddChild(newComponent);
        }

        public async Task CreateSubfolder(string id, string name)
        {
            var component = await GetTreeComponentAsync(ComponentType.Folder, id);

            var newComponent = await _folderService.CreateAsync(id, name);
            
            component.AddChild(newComponent);
        }

        private async Task<Component> GetTreeComponentAsync(ComponentType type, string id)
        {
            var root = await _builder.GetTreeAsync();
            return root.FindByIdAndType(type, id);
        }
    }
}