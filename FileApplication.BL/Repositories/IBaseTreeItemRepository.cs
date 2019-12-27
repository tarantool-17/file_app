using System.Collections.Generic;
using System.Threading.Tasks;
using FileApplication.BL.Entities;

namespace FileApplication.BL.Repositories
{
    public interface IBaseTreeItemRepository
    {
        Task<List<BaseTreeItem>> GetAllBaseAsync();
    }
}