using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FileApplication.BL.Entities;
using FileApplication.BL.Repositories;

namespace FileApplication.Data.Repositories
{
    public class FolderInMemoryRepository : IFolderRepository
    {
        private readonly ConcurrentDictionary<int, Folder> _items;

        public FolderInMemoryRepository()
        {
            _items = new ConcurrentDictionary<int, Folder>(_defaultItems.Select(x => new KeyValuePair<int, Folder>(x.Id, x)));
        }

        public async Task<List<Folder>> GetAllAsync()
        {
            return _items.Values.ToList();
        }

        public async Task<Folder> GetAsync(int id)
        {
            if (_items.TryGetValue(id, out var item))
            {
                return item;
            }
            
            throw new KeyNotFoundException();
        }

        public async Task CreateAsync(Folder folder)
        {
            _items.TryAdd(folder.Id, folder);
        }

        public async Task UpdateAsync(Folder folder)
        {
            _items.AddOrUpdate(folder.Id, folder, (key, value) => folder);
        }

        public async Task DeleteAsync(int id)
        {
            _items.TryRemove(id, out _);
        }
        
        private readonly List<Folder> _defaultItems = new List<Folder>
        {
            new Folder
            {
                Id = 1,
                Name = "Folder 1"
            },
            new Folder
            {
                Id = 2,
                Name = "Folder 2",
                ParentFolderId = 1
            }
        };
    }
}