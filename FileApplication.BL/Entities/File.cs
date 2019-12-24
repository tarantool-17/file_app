namespace FileApplication.BL.Entities
{
    public class File
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? FolderId { get; set; }
        public long Size { get; set; }
    }
}