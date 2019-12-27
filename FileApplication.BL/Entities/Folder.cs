using FileApplication.BL.Models;

namespace FileApplication.BL.Entities
{
    public class Folder: BaseTreeItem
    {
        public override ItemType Type => ItemType.Folder;
        
        public override TreeItemModel ToModel()
        {
            return new FolderModel
            {
                Id = Id,
                Name = Name,
                ParentFolderId = ParentFolderId,
                Type = Type
            };
        }
    }
}