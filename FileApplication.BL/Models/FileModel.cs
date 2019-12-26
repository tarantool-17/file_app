namespace FileApplication.BL.Models
{
    public class FileModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public long Size { get; set; }
        public int? FolderId { get; set; }
        public string Src { get; set; }
    }
}