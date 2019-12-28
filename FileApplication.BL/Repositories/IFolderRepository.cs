using System.Collections.Generic;
using System.Threading.Tasks;
using FileApplication.BL.Entities;

namespace FileApplication.BL.Repositories
{
    public interface IFolderRepository
    {
        Task<IEnumerable<Folder>> GetAllAsync();
        Task<Folder> GetAsync(string id);
        Task CreateAsync(Folder folder);
        Task RenameAsync(string id, string newName);
        Task DeleteAsync(string id);
    }
}