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
        private readonly IFolderRepository _folderRepository;
        private readonly IFileRepository _fileRepository;
        private readonly ITreeBuilder _treeBuilder;

        public TreeService(
            IFolderRepository folderRepository,
            IFileRepository fileRepository,
            ITreeBuilder treeBuilder)
        {
            _folderRepository = folderRepository;
            _fileRepository = fileRepository;
            _treeBuilder = treeBuilder;
        }

        public async Task<TreeItemModel> GetTreeAsync()
        {
            var filesTask = _fileRepository.GetAllAsync();
            var foldersTask = _folderRepository.GetAllAsync();

            await Task.WhenAll(filesTask, foldersTask);

            var files = filesTask.Result;
            var folders = foldersTask.Result;

            var children = new List<BaseTreeItem>();
            
            children.AddRange(files.Select(x =>
            {
                x.Type = ItemType.File;
                return x;
            }));
            
            children.AddRange(folders.Select(x =>
            {
                x.Type = ItemType.Folder;
                return x;
            }));

            return _treeBuilder.BuildFolderTree(children);
        }
    }
}