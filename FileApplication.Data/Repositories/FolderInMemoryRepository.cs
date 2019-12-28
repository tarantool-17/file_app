using System;
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
        private readonly ConcurrentDictionary<string, Folder> _items;

        public FolderInMemoryRepository()
        {
            _items = new ConcurrentDictionary<string, Folder>(_defaultItems.Select(x => new KeyValuePair<string, Folder>(x.Id, x)));
        }

        public async Task<IEnumerable<Folder>> GetAllAsync()
        {
            return _items.Values.ToList();
        }

        public async Task<Folder> GetAsync(string id)
        {
            if (_items.TryGetValue(id, out var item))
            {
                return item;
            }
            
            throw new KeyNotFoundException();
        }

        public async Task CreateAsync(Folder folder)
        {
            folder.Id = Guid.NewGuid().ToString();
            _items.TryAdd(folder.Id, folder);
        }

        public async Task RenameAsync(string id, string newName)
        {
            if (_items.TryGetValue(id, out var item))
            {
                _items.AddOrUpdate(id, item, (s, folder) => { 
                    folder.Name = newName;
                    return folder;
                });
                
                return;
            }
            
            throw new KeyNotFoundException();
        }

        public async Task DeleteAsync(string id)
        {
            _items.TryRemove(id, out _);
        }
        
        private readonly List<Folder> _defaultItems = new List<Folder>
        {
            new Folder
            {
                Id = "1",
                Name = "Folder 1"
            },
            new Folder
            {
                Id = "2",
                Name = "Folder 2",
                ParentFolderId = "1"
            }
        };
    }
}