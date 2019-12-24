using System.Threading.Tasks;
using FileApplication.BL.Models;
using FileApplication.BL.Services;
using Microsoft.AspNetCore.Mvc;

namespace FileApplication.Controllers
{
    [Route("folder")]
    public class FolderController : Controller
    {
        private readonly IFolderService _service;

        public FolderController(IFolderService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task CreateAsync([FromBody] FolderModel folder)
        {
            await _service.CreateAsync(folder);
        }
    }
}