using System.Collections.Generic;
using System.Threading.Tasks;
using FileApplication.BL.Models;

namespace FileApplication.BL.Services.Base
{
    public interface IComponentService
    {
        public ComponentType Type { get; }

        Task<IEnumerable<Component>> GetAllAsync();
        Task DeleteAsync(string id);
        Task RenameAsync(string id, string newName);
        Task<Component> CopyToAsync(Component component, Component parent);
    }
}