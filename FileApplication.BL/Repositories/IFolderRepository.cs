using System.Threading.Tasks;
using FileApplication.BL.Entities;

namespace FileApplication.BL.Repositories
{
    public interface IFolderRepository : IBaseTreeItemRepository
    {
        Task<Folder> GetAsync(string id);
        Task CreateAsync(Folder folder);
        Task RenameAsync(string id, string newName);
        Task DeleteAsync(string id);
    }
}