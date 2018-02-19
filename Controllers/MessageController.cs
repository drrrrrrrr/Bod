using Bod.Models.Bot;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;
using Bod.Models.Bot2;

using Newtonsoft.Json;
using Bod.Models;

namespace Bod.Controllers
{
    public class MessageController : ApiController
    {
        [Route(@"api/message/update/{id}")] //webhook uri part
        public OkResult Update([FromBody]Update update, int? id)
        {
            if (id != null)
            {
                if (update.message != null)
                {
                    SendAnswer(update,update.message.chat.id, Text(update), id);
                    return Ok();
                }
                if(update.callback_query != null)
                { 
                    AnswerIsQuery(update, id);
                    return Ok();
                }
            }
            return Ok();
        }
        string Text(Update up)
        {
            string answer = JsonConvert.SerializeObject(up);
            return answer;
        }
        public void SendAnswer(Update update,long chat_id, string message, int? id)
        {
            string reply_markup = "";
            string answer = "";
            try
            {
                answer = MainMenu(update, id, out reply_markup);
            }
            catch
            {
                answer += "  сломался";
            }
            string token = ReceiveToken(update, id);
            if (update.message.chat.id != 0)
                SendMessage(update.message.chat.id, answer, token,reply_markup);

        }

        static void SendMessage(long chat_id, string message, string token, string reply_markup = "")
        {
            string BaseUrl = "https://api.telegram.org/bot";
            string address = BaseUrl + token + "/sendMessage";
            NameValueCollection nvc = new NameValueCollection();
            nvc.Add("chat_id", chat_id.ToString());
            nvc.Add("text", message);
            if (reply_markup != "")
            {
                nvc.Add("reply_markup", reply_markup);
            }
            using (WebClient client = new WebClient())
                client.UploadValues(address, nvc);
        }

        string ReceiveToken(Update update,int? id)
        {
            string token;
            using (botEntities1 bot = new botEntities1())
                token = bot.Token.Where(x => x.id == id).First().token1;
            return token;
        }
        void ChangeMessage(Update update, string message, int? id, string reply_markup = "")
        {
            string token = ReceiveToken(update, id);
            string BaseUrl = "https://api.telegram.org/bot";

            string adress = BaseUrl + token + "/editMessageText";
            NameValueCollection nvc = new NameValueCollection();
            nvc.Add("chat_id", update.callback_query.message.chat.id.ToString());
            nvc.Add("message_id", update.callback_query.message.message_id.ToString());
            nvc.Add("text", message);
            if (reply_markup != "")
                nvc.Add("reply_markup", reply_markup);
            using (WebClient client = new WebClient())
                client.UploadValues(adress, nvc);
        }

        void AnswerIsQuery(Update update,int? id)
        {
            string reply_markup = "";
            string answer = "ll";
            
            switch(update.callback_query.data)
            {
                case "?":
                    answer = "Вопрос!";
                    MainMenu(update,id,out reply_markup);
                    break;
                case "about":
                    answer = "Что-нибудь можно написать";
                    MainMenu(update, id, out reply_markup);
                    break;
                default:
                    answer = Shop(update.callback_query.data , update, id, out reply_markup); 
                    break;
            }
            answer += Environment.NewLine + update.callback_query.data;
            ChangeMessage(update, answer, id, reply_markup);
        }
        void AddMainButtons(InlineKeyboard keyboard)
        {
            List<InlineKeyboardButton> line = new List<InlineKeyboardButton>()
            {
                new InlineKeyboardButton("Есть вопрос", "?" ),
                new InlineKeyboardButton("О нас", "about" )
            };
            keyboard.AddLine(line);
        }
        string Shop(string shop,Update update, int? id,out string reply_markup)
        {
            string category = shop.Split(' ')[0];
            string nameProduct = shop.Split(' ').Length < 2 ? "" : shop.Split(' ')[1];
            string answer="Привет";
            reply_markup = "";
            List<Category> cat=null;
            List<Product> p=null;
           // SendMessage(update.callback_query.from.id, category , ReceiveToken(update, id));
            using (botEntities1 bd = new botEntities1())
            {
                try
                {
                    cat = bd.Category.Where(x => x.Token.id == id).ToList();
                    p= cat.Where(x => x.NameCategory == category).First().Product.ToList();
                    if (nameProduct == "") nameProduct = p[0].ProductName;
                }
                catch
                {
              //   SendMessage(update.callback_query.from.id, "БД УПало", ReceiveToken(update, id));
                }
            }

            //try
            //{
            //    SendMessage(update.callback_query.from.id, p.Count.ToString(), ReceiveToken(update, id));
            //}
            //catch
            //{
            //    SendMessage(update.callback_query.from.id, "равно 0", ReceiveToken(update, id));
            //}
            //list = cat.Where(x => x.NameCategory == category).First();
            //if (list == null)
            //{
            //    SendMessage(update.callback_query.from.id, "null", ReceiveToken(update, id));
            //}
            //else SendMessage(update.callback_query.from.id, list.Product.Count.ToString(), ReceiveToken(update, id));
            InlineKeyboard keyboard = new InlineKeyboard();
            int i = 0;
            foreach (Product k in p)
            {
                keyboard.AddButton(new InlineKeyboardButton(

                  k.ProductName,k.Category.NameCategory + " " + k.ProductName), i++ / 2);
            }

            Product chooseProduct = p.Where(x => x.ProductName == nameProduct).First();
            answer += chooseProduct.ProductPrice + Environment.NewLine + "  " + " " + chooseProduct.ProductDescription;
            AddMainButtons(keyboard);
            reply_markup = JsonConvert.SerializeObject(keyboard);
            return answer;
        }
        string MainMenu(Update update,int? id,out string reply_markup)
        {
            List<Category> list;
            string token = "";
            int i = 0;
            using (botEntities1 bd = new botEntities1())
            {
                list = bd.Category.Where(x => x.Token.id == id).ToList();
                token = bd.Token.Where(x => x.id == id).First().token1;
            }
            InlineKeyboard keyboard = new InlineKeyboard();
            foreach(Category k in list)
            {
                keyboard.AddButton(new InlineKeyboardButton(
                    k.NameCategory
                    ),i++/2);
            }
            AddMainButtons(keyboard); 
            reply_markup = JsonConvert.SerializeObject(keyboard);
            return "Все категории";
        }
     
      
       
        //    string Shop(string shop, out string reply_markup)
        //    {
        //        string answer = "Магазин продуктов";
        //        reply_markup = "";
        //        Category categories;
        //        char[] spl = { ' ' };
        //        string[] shops = shop.Split(spl, StringSplitOptions.RemoveEmptyEntries);
        //        string ncat = shops[0];
        //        string nproduct = "q";
        //        if (shops.Length > 1)
        //            nproduct = shops[1];
        //        Product pr = new Product();
        //        List<InlineKeyboardButton> kb = new List<InlineKeyboardButton>();
        //        using (UpdateDbContext db = new UpdateDbContext())
        //        {
        //            if (shops.Length == 1)
        //            {
        //                var product = db.Products.ToList();
        //                categories = db.Categorys.Where(x => x.NameCategory == ncat).First();
        //            }
        //            else
        //            {
        //                var product = db.Products.ToList();
        //                //categories = db.Categorys.Where(x => x.NameCategory == ncat).First();
        //                categories = db.Categorys.Where(x => x.NameCategory == ncat).First();
        //                pr = categories.Products.Where(x => x.NameProduct == nproduct).First();
        //            }
        //        }
        //        InlineKeyboard keyboard = new InlineKeyboard();
        //        int i = 0;
        //        if (shops.Length == 1)
        //        {
        //            foreach (var item in categories.Products)
        //            {
        //                keyboard.AddButton(new InlineKeyboardButton(item.NameProduct, item.Category.NameCategory + " " + item.NameProduct), i++ / 2);
        //            }
        //        }
        //        if (shops.Length > 1)
        //        {
        //            foreach (var item in categories.Products)
        //            {
        //                keyboard.AddButton(new InlineKeyboardButton(item.NameProduct, item.Category.NameCategory + " " + item.NameProduct), i++ / 2);
        //            }
        //            answer = pr.NameProduct + "    " + pr.Price + "   " + pr.Description;
        //        }
        //        AddMainButton(keyboard);
        //        reply_markup = JsonConvert.SerializeObject(keyboard);


        //        return answer;
        //    }
        //    void AnswerIqQuery(Result item)
        //    {
        //        //   SendMessage(item.callback_query.from.id, "Здарова");
        //        //MenuFrombd(item.callback_query.from.id);
        //        // ChangeMessage(item);

        //        string answer = "";
        //        string replyMarkup = "";
        //        switch (item.callback_query.data)
        //        {
        //            case "?": answer = "Вопрос в лс"; MainMenu(out replyMarkup); break;
        //            case "about": answer = "Уэто магазин"; MainMenu(out replyMarkup); break;
        //            default:
        //                answer = Shop(item.callback_query.data, out replyMarkup);
        //                break;
        //        }
        //        answer += Environment.NewLine + item.callback_query.data;
        //        ChangeMessage(item, answer, replyMarkup);
        //    }

    }
}