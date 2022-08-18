using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ZeroWindApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CodeController : ControllerBase
    {
        public CodeController()
        {

        }

        [HttpGet]
        public Task<IActionResult> Get()
        {

        }
    }
}
