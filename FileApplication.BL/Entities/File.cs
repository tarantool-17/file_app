namespace FileApplication.BL.Entities
{
    public class File : BaseEntity
    {
        public string Src { get; set; }
        public long Size { get; set; }
    }
}