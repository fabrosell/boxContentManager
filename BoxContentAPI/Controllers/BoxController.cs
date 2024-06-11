using Microsoft.AspNetCore.Mvc;

namespace BoxContentAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BoxController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
