using System.Threading.Tasks;
using FileApplication.BL.Entities;

namespace FileApplication.BL.Services
{
    public interface IItemActionService
    {
        ItemType Type { get; }
        
        Task DeleteAsync(string id);
        Task RenameAsync(string id, string name);
        Task CopyAsync(string id, string parentId);
    }
}