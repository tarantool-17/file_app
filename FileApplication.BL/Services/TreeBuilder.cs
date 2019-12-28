using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FileApplication.BL.Models;
using FileApplication.BL.Services.Base;

namespace FileApplication.BL.Services
{
    public interface ITreeBuilder
    {
        Task<Component> BuildFolderTree();
    }
    
    public class TreeBuilder : ITreeBuilder
    {
        private readonly IComponentServiceFactory _serviceFactory;
        
        public TreeBuilder(IComponentServiceFactory serviceFactory)
        {
            _serviceFactory = serviceFactory;
        }
        
        public async Task<Component> BuildFolderTree()
        {
            var root = new FolderComponent
            {
                Name = "root"
            };

            var items = await GetTreeItemsAsync();

            PopulateNode(root, items);

            return root;
        }
        
        private async Task<List<Component>> GetTreeItemsAsync()
        {
            var tasks = _serviceFactory
                .GetAllComponentServices()
                .Select(x => x.GetAllAsync())
                .ToList();
            
            await Task.WhenAll(tasks);

            var items = new List<Component>();
            foreach (var task in tasks)
            {
                items.AddRange(await task);
            }

            return items;
        }

        private void PopulateNode(Component node, List<Component> items)
        {
            node.Children = items?
                .Where(x => x.ParentId == node.Id)
                .ToList();

            var childFolders = node.Children?
                .Where(x => x.Type == ComponentType.Folder);

            if (!(childFolders?.Any() ?? false))
                return;

            foreach (var folder in childFolders)
            {
                PopulateNode(folder, items);
            }
        }
    }
}