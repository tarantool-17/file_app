using FileApplication.BL.Entities;
using FileApplication.BL.Models;

namespace FileApplication.BL.Extensions
{
    public static class Extensions
    {
        public static FileModel ToModel(this File file)
        {
            return new FileModel
            {
                Id = file.Id,
                Name = file.Name,
                Size = file.Size,
                FolderId = file.ParentFolderId,
                Src = file.Src
            };
        }
        
        public static File ToEntity(this FileModel file)
        {
            return new File
            {
                Id = file.Id,
                Name = file.Name,
                Size = file.Size,
                ParentFolderId = file.FolderId,
                Src = file.Src
            };
        }

        public static FolderModel ToModel(this Folder folder)
        {
            return new FolderModel
            {
                Id = folder.Id,
                Name = folder.Name,
                Type = ItemType.Folder
            };
        }
        
        public static Folder ToEntity(this FolderModel folder)
        {
            return new Folder
            {
                Id = folder.Id ?? -1,
                Name = folder.Name,
                Type = ItemType.Folder,
                ParentFolderId = folder.ParentFolderId
            };
        }

        public static TreeItemModel ToModel(this BaseTreeItem item)
        {
            return new TreeItemModel
            {
                Id = item.Id,
                Name = item.Name,
                Type = item.Type,
                ParentFolderId = item.ParentFolderId
            };
        }
    }
}