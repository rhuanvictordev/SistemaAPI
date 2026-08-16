using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace RestauranteAPI.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}
