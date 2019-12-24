using System.Threading.Tasks;
using FileApplication.BL.Entities;
using FileApplication.BL.Services;
using Microsoft.AspNetCore.Mvc;

namespace FileApplication.Controllers
{
    public class ItemController : Controller
    {
        private readonly IItemFactory _factory;

        public ItemController(IItemFactory factory)
        {
            _factory = factory;
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