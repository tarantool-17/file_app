using System.Threading.Tasks;
using FileApplication.BL.Models;
using FileApplication.BL.Services;
using Microsoft.AspNetCore.Mvc;

namespace FileApplication.Controllers
{
    public class ComponentController : Controller
    {
        private readonly IFacade _facade;

        public ComponentController(IFacade facade)
        {
            _facade = facade;
        }

        [HttpGet("tree")]
        public async Task<Component> GetFullTreeAsync()
        {
            var res = await _facade.GetTreeAsync();

            return res;
        }

        [HttpDelete("{type}/{id}")]
        public async Task DeleteAsync(ComponentType type, string id)
        {
            await _facade.DeleteAsync(type, id);
        }
        
        [HttpPut("{type}/{id}/{name}")]
        public async Task RenameAsync(ComponentType type, string id, string name)
        {
            await _facade.RenameAsync(type, id, name);
        }
        
        [HttpPut("{type}/{id}/{parentId}")]
        public async Task CopyAsync(ComponentType type, string id)
        {
            await _facade.CopyAsync(type, id);
        }
    }
}