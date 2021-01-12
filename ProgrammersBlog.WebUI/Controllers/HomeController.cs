using Microsoft.AspNetCore.Mvc;

namespace ProgrammersBlog.WebUI.Controllers
{
    public class HomeController : Controller
    {
        // GET
        public IActionResult Index()
        {
            return View();
        }
    }
}