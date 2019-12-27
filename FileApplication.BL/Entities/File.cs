using FileApplication.BL.Models;

namespace FileApplication.BL.Entities
{
    public class File : BaseTreeItem
    {
        public override ItemType Type => ItemType.File;

        public long Size { get; set; }
        
        public override TreeItemModel ToModel()
        {
            return new FileModel
            {
                Id = Id,
                Name = Name,
                Size = Size,
                ParentFolderId = ParentFolderId,
                Type = Type
            };
        }
    }
}