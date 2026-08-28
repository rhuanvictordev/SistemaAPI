using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestauranteAPI.Controllers
{
    public class AuthController : Controller
    {
        public ActionResult Register()
        { 
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }
    }
}
