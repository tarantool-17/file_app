namespace FileApplication.BL.Entities
{
    public abstract class BaseEntity
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string ParentFolderId { get; set; }
    }
}