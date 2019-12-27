using FileApplication.BL.Models;

namespace FileApplication.BL.Entities
{
    public abstract class BaseTreeItem
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string ParentFolderId { get; set; }
        
        //TODO: Should be ignored by ORM.
        public abstract ItemType Type { get; }
        public abstract TreeItemModel ToModel();
    }
}