using FileApplication.BL.Entities;
using FileApplication.BL.Models;

namespace FileApplication.BL.Extensions
{
    public static class Extensions
    {
        public static FileModel ToFileModel(this File file)
        {
            return new FileModel
            {
                Id = file.Id,
                Name = file.Name,
                Size = file.Size,
                FolderId = file.FolderId
            };
    }
    }
}