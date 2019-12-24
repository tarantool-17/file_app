using System.Threading.Tasks;
using FileApplication.BL.Entities;

namespace FileApplication.BL.Repositories
{
    public interface IFileRepository
    {
        Task<File> GetAsync(int id);
        Task CreateAsync(File file);
        Task UpdateAsync(File file);
        Task DeleteAsync(int id);
    }
}