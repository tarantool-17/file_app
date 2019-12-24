using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FileApplication.BL.Entities;
using FileApplication.BL.Repositories;

namespace FileApplication.Data.Repositories
{
    public class FileInMemoryRepository : IFileRepository
    {
        private readonly ConcurrentDictionary<int, File> _items;

        public FileInMemoryRepository()
        {
            _items = new ConcurrentDictionary<int, File>(_defaultFiles.Select(x => new KeyValuePair<int, File>(x.Id, x)));
        }
        
        public async Task<File> GetAsync(int id)
        {
            if (_items.TryGetValue(id, out var item))
            {
                return item;
            }
            
            throw new KeyNotFoundException();
        }

        public async Task CreateAsync(File file)
        {
            _items.TryAdd(file.Id, file);
        }

        public async Task UpdateAsync(File file)
        {
            _items.AddOrUpdate(file.Id, file, (key, value) => file);
        }

        public async Task DeleteAsync(int id)
        {
            _items.TryRemove(id, out _);
        }
        
        private readonly List<File> _defaultFiles = new List<File>
        {
            new File
            {
                Id = 1,
                Name = "test1.txt",
                Size = 100
            },
            new File
            {
                Id = 2,
                Name = "test2.txt",
                Size = 200,
                FolderId = 1
            },
            new File
            {
                Id = 3,
                Name = "test3.txt",
                Size = 300,
                FolderId = 1
            },
            new File
            {
                Id = 4,
                Name = "test4.txt",
                Size = 50,
                FolderId = 2
            }
        };
    }
}