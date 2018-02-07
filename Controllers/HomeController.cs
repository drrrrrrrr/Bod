using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BotShopOfficial.Controllers
{

    //Для нашего лэндинга
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}