using Microsoft.AspNetCore.Mvc;

namespace Foo.Api.Controllers
{
    public class SongsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
