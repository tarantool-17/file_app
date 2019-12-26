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
        
        public Task DeleteAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        public async Task RenameAsync(int id, string name)
        {
            //TODO: Rename without get.
            
            var folder = await _repository.GetAsync(id);

            if (folder == null)
                throw new KeyNotFoundException();

            folder.Name = name;
            
            await _repository.UpdateAsync(folder);
        }

        public Task CopyAsync(int id, int? parentId)
        {
            throw new System.NotImplementedException();
        }

        public async Task CreateAsync(FolderModel model)
        {
            await _repository.CreateAsync(model.ToEntity());
        }
    }
}