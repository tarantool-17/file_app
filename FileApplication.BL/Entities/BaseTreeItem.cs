namespace FileApplication.BL.Entities
{
    public class BaseTreeItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ItemType Type { get; set; }
        public int? ParentFolderId { get; set; }
    }
}