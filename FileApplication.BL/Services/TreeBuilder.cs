using System.Collections.Generic;
using System.Linq;
using FileApplication.BL.Entities;
using FileApplication.BL.Models;

namespace FileApplication.BL.Services
{
    public interface ITreeBuilder
    {
        TreeItemModel BuildFolderTree(List<BaseTreeItem> items);
    }
    
    public class TreeBuilder : ITreeBuilder
    {
        public TreeItemModel BuildFolderTree(List<BaseTreeItem> items)
        {
            var root = new TreeItemModel
            {
                Name = "root",
                Type = ItemType.Folder
            };

            PopulateNode(root, items);

            return root;
        }

        private void PopulateNode(TreeItemModel node, List<BaseTreeItem> items)
        {
            node.Children = items?
                .Where(x => x.ParentFolderId == node.Id)
                .Select(x => x.ToModel())
                .ToList();

            var childFolders = node.Children?
                .Where(x => x.Type == ItemType.Folder);

            if (!(childFolders?.Any() ?? false))
                return;

            foreach (var folder in childFolders)
            {
                PopulateNode(folder, items);
            }
        }
    }
}