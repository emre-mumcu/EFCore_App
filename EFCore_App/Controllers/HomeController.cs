using Microsoft.AspNetCore.Mvc;

namespace EFCore_App.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return Content("Index");
        }
    }
}
