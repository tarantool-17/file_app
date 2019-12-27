using FileApplication.BL.Entities;
using FileApplication.BL.Models;

namespace FileApplication.BL.Extensions
{
    public static class Extensions
    {
        public static File ToEntity(this FileModel file)
        {
            return new File
            {
                Id = file.Id,
                Name = file.Name,
                Size = file.Size,
                ParentFolderId = file.ParentFolderId
            };
        }
        
        public static Folder ToEntity(this FolderModel folder)
        {
            return new Folder
            {
                Id = folder.Id,
                Name = folder.Name,
                ParentFolderId = folder.ParentFolderId
            };
        }
    }
}