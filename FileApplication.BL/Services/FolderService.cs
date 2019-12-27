using System.Collections.Generic;
using System.Threading.Tasks;
using FileApplication.BL.Entities;
using FileApplication.BL.Extensions;
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
        private readonly IFileRepository _fileRepository;
        
        public ItemType Type => ItemType.Folder;

        public FolderService(IFolderRepository repository, IFileRepository fileRepository)
        {
            _repository = repository;
            _fileRepository = fileRepository;
        }
        
        public Task DeleteAsync(string id)
        {
            throw new System.NotImplementedException();
        }

        public async Task RenameAsync(string id, string name)
        {
            await _repository.RenameAsync(id, name);
        }

        public Task CopyAsync(string id, string parentId)
        {
            throw new System.NotImplementedException();
        }

        public async Task CreateAsync(FolderModel model)
        {
            await _repository.CreateAsync(model.ToEntity());
        }
    }
}