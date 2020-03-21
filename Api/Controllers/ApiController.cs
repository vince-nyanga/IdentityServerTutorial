using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    public class ApiController : Controller
    {
        [Route("/secret")]
        [Authorize]
        public IActionResult Index()
        {
            return new JsonResult(new
            {
                secret = "I love ASP.NET Core"
            });
        }
    }
}