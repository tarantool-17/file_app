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
            var res = await _service.GetTreeAsync();

            return res;
        }

        [HttpDelete("{type}/{id}")]
        public async Task DeleteAsync(ItemType type, string id)
        {
            var service = _factory.GetItemService(type);
            await service.DeleteAsync(id);
        }
        
        [HttpPut("{type}/{id}/{name}")]
        public async Task RenameAsync(ItemType type, string id, string name)
        {
            var service = _factory.GetItemService(type);
            await service.RenameAsync(id, name);
        }
        
        [HttpPut("{type}/{id}/{parentId}")]
        public async Task CopyAsync(ItemType type, string id, string parentId)
        {
            var service = _factory.GetItemService(type);
            await service.CopyAsync(id, parentId);
        }
    }
}