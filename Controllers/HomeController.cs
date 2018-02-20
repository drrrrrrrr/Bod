using Bod;
using Bod.Models;
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
            List<Category> cat;
            List<Product> p;
            //SendMessage(update.callback_query.from.id, category , ReceiveToken(update, id));
            //using (botEntities1 bd = new botEntities1())
            //{

            //    cat = bd.Category.ToList();
            //    p = cat.Where(x => x.NameCategory == "Category1M1").First().Product.ToList();
            //}
            
            string m;
            return View();
        }
   
        public string About()
        {
            //List<string> tokens = new List<string>();
            //tokens.Add("513919028:AAH3YpNoZAeZwo5NItLEmIXUhobfR5ziSRU");
            //tokens.Add("523703359:AAHGsDDzW7ZS0iQXWsJ_g8X7DeldpFVy4qQ");
            ////SetWebHook("501412002:AAGLeNPhy3icrSfs4WbY0QhIRMwLULbmqDo");
            ////SetWebHook("523703359:AAHGsDDzW7ZS0iQXWsJ_g8X7DeldpFVy4qQ");
            //for (int i = 0; i < tokens.Count; i++)
            //{
            //    SetWebHook(tokens[i], i);
            //}
            string k = "1";
            using (botEntities2 bd = new botEntities2())
            {
                //Token a = new Token();
                //    a.token1 = "513919028:AAH3YpNoZAeZwo5NItLEmIXUhobfR5ziSRU";
                //    a.id = 1;
                //    bd.Token.Add(a);
                //    SetWebHook(a.token1, a.id);
                //    Token b = new Token();
                //     b.token1 = "523703359:AAHGsDDzW7ZS0iQXWsJ_g8X7DeldpFVy4qQ";
                //    bd.Token.Add(b);
                //    SetWebHook(b.token1, b.id);
                //    k = "12";

                //bd.SaveChanges();
                Token m = bd.Token.Where(x => x.Id == 1).First();
                SetWebHook(m.token1, m.Id);
                Token m2 = bd.Token.Where(x => x.Id == 3).First();
                SetWebHook(m2.token1, m2.Id);

            }
            return "Сработало" + k ;
        }
        static string SetWebHook(string token, int i)
        {
          
            string BaseUrl = "https://api.telegram.org/bot";
            string adress = BaseUrl + token + "/setWebhook";
            NameValueCollection nvc = new NameValueCollection();
            WebClient client = new WebClient();
            string addresTo = @"https://createbotshop.azurewebsites.net/api/message/update/" + i ;
            nvc.Add("url", addresTo);
            client.UploadValues(adress, nvc);
            return addresTo;
        }
    }
}