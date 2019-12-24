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
    }
}