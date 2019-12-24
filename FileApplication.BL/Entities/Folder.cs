namespace FileApplication.BL.Entities
{
    public class Folder
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int? ParentFolderId { get; set; }
    }
}