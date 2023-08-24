using Microsoft.AspNetCore.Mvc;

namespace BulkyBookWeb.Areas.Persons.Controllers
{
    public class HomeController : Controller
    {
        [Area("Persons")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
