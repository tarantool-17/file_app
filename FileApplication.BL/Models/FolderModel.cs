namespace FileApplication.BL.Models
{
    public class FolderModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int? ParentFolderId { get; set; }
    }
}