using System.Threading.Tasks;
using FileApplication.BL.Entities;
using FileApplication.BL.Models;
using FileApplication.BL.Services;
using Microsoft.AspNetCore.Mvc;

namespace FileApplication.Controllers
{
    public class TreeItemController : Controller
    {
        private readonly IItemFactory _factory;
        private readonly ITreeService _service;

        public TreeItemController(IItemFactory factory, ITreeService service)
        {
            _factory = factory;
            _service = service;
        }

        [HttpGet("tree")]
        public async Task<TreeItemModel> GetFullTreeAsync()
        {
            return await _service.GetTreeAsync();
        }

        [HttpDelete("{type}/{id}")]
        public async Task DeleteAsync(ItemType type, int id)
        {
            var service = _factory.GetItemService(type);
            await service.DeleteAsync(id);
        }
        
        [HttpPut("{type}/{id}/{name}")]
        public async Task RenameAsync(ItemType type, int id, string name)
        {
            var service = _factory.GetItemService(type);
            await service.RenameAsync(id, name);
        }
        
        [HttpPut("{type}/{id}/{parentId}")]
        public async Task CopyAsync(ItemType type, int id, int? parentId)
        {
            var service = _factory.GetItemService(type);
            await service.CopyAsync(id, parentId);
        }
    }
}