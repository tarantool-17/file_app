using System.Threading.Tasks;
using FileApplication.BL.Models;
using FileApplication.BL.Services;
using FileApplication.Models;
using Microsoft.AspNetCore.Mvc;

namespace FileApplication.Controllers
{
    [Route("component")]
    public class ComponentController : Controller
    {
        private readonly IFacade _facade;

        public ComponentController(IFacade facade)
        {
            _facade = facade;
        }

        [HttpDelete("{type}/{id}")]
        public async Task DeleteAsync(ComponentType type, string id)
        {
            await _facade.DeleteAsync(type, id);
        }
        
        [HttpPut("rename")]
        public async Task RenameAsync([FromBody] RenameModel model)
        {
            await _facade.RenameAsync(model.Type, model.Id, model.NewName);
        }
        
        [HttpPut("copy")]
        public async Task CopyAsync([FromBody] ComponentBase model)
        {
            await _facade.CopyAsync(model.Type, model.Id);
        }
    }
}