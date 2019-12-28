﻿using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FileApplication.BL.Entities;
using FileApplication.BL.Repositories;

namespace FileApplication.Data.Repositories
{
    public class FileInMemoryRepository : IFileRepository
    {
        private readonly ConcurrentDictionary<string, File> _items;

        public FileInMemoryRepository()
        {
            _items = new ConcurrentDictionary<string, File>(_defaultFiles.Select(x => new KeyValuePair<string, File>(x.Id, x)));
        }

        public async Task<IEnumerable<File>> GetAllAsync()
        {
            return _items.Values.ToList();
        }

        public async Task<File> GetAsync(string id)
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

        public async Task RenameAsync(string id, string newName)
        {
            if (_items.TryGetValue(id, out var item))
            {
                _items.AddOrUpdate(id, item, (s, file) => { 
                    file.Name = newName;
                    return file;
                });
                
                return;
            }
            
            throw new KeyNotFoundException();
        }

        public async Task DeleteAsync(string id)
        {
            _items.TryRemove(id, out _);
        }
        
        private readonly List<File> _defaultFiles = new List<File>
        {
            new File
            {
                Name = "test1.txt",
                Size = 100,
                Id = "/test1.txt"
            },
            new File
            {
                Name = "test2.txt",
                Size = 200,
                ParentFolderId = "1",
                Id = "/test2.txt"
            },
            new File
            {
                Name = "test3.txt",
                Size = 300,
                ParentFolderId = "1",
                Id = "/test3.txt"
            },
            new File
            {
                Name = "test4.txt",
                Size = 50,
                ParentFolderId = "2",
                Id = "/test4.txt"
            }
        };
    }
}