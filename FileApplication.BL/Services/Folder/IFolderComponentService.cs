using System.Threading.Tasks;
using FileApplication.BL.Models;
using FileApplication.BL.Services.Base;

namespace FileApplication.BL.Services
{
    public interface IFolderComponentService : IComponentService
    {
        Task<FolderComponent> CreateAsync(string parentId, string name);
    }
}