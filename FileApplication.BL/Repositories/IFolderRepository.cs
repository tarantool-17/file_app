﻿using System.Threading.Tasks;
using FileApplication.BL.Entities;

namespace FileApplication.BL.Repositories
{
    public interface IFolderRepository
    {
        Task<Folder> GetAsync(int id);
        Task CreateAsync(Folder folder);
        Task UpdateAsync(Folder folder);
        Task DeleteAsync(int id);
    }
}