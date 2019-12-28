using System.Threading.Tasks;
using FileApplication.BL.Services;
using FileApplication.Models;
using Microsoft.AspNetCore.Mvc;

namespace FileApplication.Controllers
{
    [Route("folder")]
    public class FolderController : Controller
    {
        private readonly IFacade _facade;

        public FolderController(IFacade facade)
        {
            _facade = facade;
        }

        [HttpPost]
        public async Task CreateAsync([FromBody] FolderModel model)
        {
            await _facade.CreateSubfolder(model.ParentId, model.Name);
        }
    }
}