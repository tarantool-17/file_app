using System.IO;
using System.Threading.Tasks;
using FileApplication.BL.Models;
using FileApplication.BL.Services;
using Microsoft.AspNetCore.Mvc;

namespace FileApplication.Controllers
{
    [Route("file")]
    public class FileController : Controller
    {
        private readonly IFileService _service;

        public FileController(IFileService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task UploadAsync([FromBody]FileModel model)
        {
            await _service.UploadFileAsync(model, null);
        }
        
        [HttpGet("{id}")]
        public async Task<IActionResult> Download(int id)
        {
            var stream = await _service.DownloadAsync(id);

            if(stream == null)
                return NotFound(); 

            return File(stream, "application/octet-stream");
        }    
    }
}