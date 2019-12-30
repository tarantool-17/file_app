using System.Threading.Tasks;
using FileApplication.BL.Models;
using FileApplication.BL.Services;
using Microsoft.AspNetCore.Mvc;

namespace FileApplication.Controllers
{
    public class TreeController : Controller
    {
        private readonly IFacade _facade;

        public TreeController(IFacade facade)
        {
            _facade = facade;
        }

        [HttpGet("tree")]
        public async Task<Component> GetFullTreeAsync()
        {
            var res = await _facade.GetTreeAsync();

            return res;
        }
    }
}