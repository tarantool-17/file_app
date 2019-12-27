using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FileApplication.BL.Entities;
using FileApplication.BL.Models;
using FileApplication.BL.Repositories;

namespace FileApplication.BL.Services
{
    public interface ITreeService
    {
        Task<TreeItemModel> GetTreeAsync();
    }
    
    public class TreeService : ITreeService
    {
        private readonly IEnumerable<IBaseTreeItemRepository> _repositories;
        private readonly ITreeBuilder _treeBuilder;

        public TreeService(IEnumerable<IBaseTreeItemRepository> repositories, ITreeBuilder treeBuilder)
        {
            _repositories = repositories;
            _treeBuilder = treeBuilder;
        }

        public async Task<TreeItemModel> GetTreeAsync()
        {
            var tasks = _repositories.Select(x => x.GetAllBaseAsync()).ToList();
            
            await Task.WhenAll(tasks);

            var items = new List<BaseTreeItem>();
            foreach (var task in tasks)
            {
                items.AddRange(await task);
            }

            return _treeBuilder.BuildFolderTree(items);
        }
    }
}