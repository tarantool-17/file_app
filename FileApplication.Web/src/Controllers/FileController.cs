using System.Threading.Tasks;
using FileApplication.BL.Services;
using FileApplication.Models;
using Microsoft.AspNetCore.Mvc;

namespace FileApplication.Controllers
{
    [Route("file")]
    public class FileController : Controller
    {
        private readonly IFacade _facade;

        public FileController(IFacade facade)
        {
            _facade = facade;
        }

        [HttpPost]
        public async Task UploadAsync([FromBody]FileModel model)
        {
            await _facade.UploadFileAsync(model.ParentId, model.Name, null);
        }
        
        [HttpGet("{id}")]
        public async Task<IActionResult> Download(string id)
        {
            var stream = await _facade.DownloadFileAsync(id);

            if(stream == null)
                return NotFound(); 

            return File(stream, "application/octet-stream");
        }    
    }
}