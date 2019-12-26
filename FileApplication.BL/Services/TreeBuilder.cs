using System.Collections.Generic;
using System.Linq;
using FileApplication.BL.Entities;
using FileApplication.BL.Extensions;
using FileApplication.BL.Models;

namespace FileApplication.BL.Services
{
    public interface ITreeBuilder
    {
        TreeItemModel BuildFolderTree(List<BaseTreeItem> children);
    }
    
    public class TreeBuilder : ITreeBuilder
    {
        public TreeItemModel BuildFolderTree(List<BaseTreeItem> children)
        {
            var root = new TreeItemModel
            {
                Name = "root",
                Type = ItemType.Folder
            };

            PopulateNode(root, children);

            return root;
        }

        private void PopulateNode(TreeItemModel node, List<BaseTreeItem> children)
        {
            node.Children = children?
                .Where(x => x.ParentFolderId == node.Id)
                .Select(x => x.ToModel())
                .ToList();

            var childFolders = node.Children?
                .Where(x => x.Type == ItemType.Folder);

            if (!(childFolders?.Any() ?? false))
                return;

            foreach (var folder in childFolders)
            {
                PopulateNode(folder, children);
            }
        }
    }
}