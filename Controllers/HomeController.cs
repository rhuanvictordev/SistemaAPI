using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace SistemaAPI.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}
