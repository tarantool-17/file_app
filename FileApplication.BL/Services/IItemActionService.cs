using System.Threading.Tasks;
using FileApplication.BL.Entities;

namespace FileApplication.BL.Services
{
    public interface IItemActionService
    {
        ItemType Type { get; }
        
        Task DeleteAsync(int id);
        Task RenameAsync(int id, string name);
        Task CopyAsync(int id, int? parentId);
    }
}