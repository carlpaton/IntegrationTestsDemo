using Microsoft.AspNetCore.Mvc;

namespace Foo.Api.Controllers
{
    public class PackagesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
