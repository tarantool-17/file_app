using System.Threading.Tasks;
using FileApplication.BL.Entities;
using FileApplication.BL.Models;
using FileApplication.BL.Repositories;

namespace FileApplication.BL.Services
{
    public interface IFolderService : IItemActionService
    {
        Task CreateAsync(FolderModel model);
    }
    
    public class FolderService : IFolderService
    {
        private readonly IFolderRepository _repository;
        
        public ItemType Type => ItemType.Folder;

        public FolderService(IFolderRepository repository)
        {
            _repository = repository;
        }
        
        public Task DeleteAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task RenameAsync(int id, string name)
        {
            throw new System.NotImplementedException();
        }

        public Task CopyAsync(int id, int? parentId)
        {
            throw new System.NotImplementedException();
        }

        public Task CreateAsync(FolderModel model)
        {
            throw new System.NotImplementedException();
        }
    }
}