using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
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
        public string About()
        {
            SetWebHook("501412002:AAGLeNPhy3icrSfs4WbY0QhIRMwLULbmqDo");
            return "DA";
        }
       static void SetWebHook(string token)
        {
       
            string BaseUrl = "https://api.telegram.org/bot";
            string adress = BaseUrl + token + "/setWebhook";
            NameValueCollection nvc = new NameValueCollection();
            WebClient client = new WebClient();
            nvc.Add("url", "https://createbotshop.azurewebsites.net/api/message/update");
            client.UploadValues(adress, nvc);
        }
    }
}