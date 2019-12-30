using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FileApplication.BL.Models;
using FileApplication.BL.Services.Base;

namespace FileApplication.BL.Services
{
    public interface ITreeBuilder
    {
        Task<Component> GetTreeAsync(bool isRebuild = false);
    }
    
    public class TreeBuilder : ITreeBuilder
    {
        private Component _root;
        private readonly IComponentServiceFactory _serviceFactory;
        
        public TreeBuilder(IComponentServiceFactory serviceFactory)
        {
            _serviceFactory = serviceFactory;
        }
        
        public async Task<Component> GetTreeAsync(bool isRebuild = false)
        {
            if (_root == null || isRebuild)
            {
                _root = new FolderComponent
                {
                    Name = "root"
                };

                var items = await GetTreeItemsAsync();

                PopulateNode(_root, items);
            }

            return _root;
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

            if (node.Children == null) 
                return;
            
            foreach (var child in node.Children)
            {
                child.Parent = node;

                if (child.Type == ComponentType.Folder)
                {
                    PopulateNode(child, items);
                }
            }
        }
    }
}