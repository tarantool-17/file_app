using System.Threading.Tasks;
using FileApplication.BL.Entities;

namespace FileApplication.BL.Repositories
{
    public interface IFileRepository : IBaseTreeItemRepository
    {
        Task<File> GetAsync(string id);
        Task CreateAsync(File file);
        Task RenameAsync(string id, string newName);
        Task DeleteAsync(string id);
    }
}