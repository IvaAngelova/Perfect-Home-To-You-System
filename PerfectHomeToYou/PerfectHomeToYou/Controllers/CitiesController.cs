using Microsoft.AspNetCore.Mvc;

namespace PerfectHomeToYou.Controllers
{
    public class CitiesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
