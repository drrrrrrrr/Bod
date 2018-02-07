using Bod.Models.Bot;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;

namespace Bod.Controllers
{
    public class MessageController : ApiController
    {
        [Route(@"api/message/update")] //webhook uri part
        public  OkResult Update([FromBody]Update update)
        {

            long chat_id = update.message.chat.id;
            SendMessage(chat_id, "Привет");

            return Ok();
        }
        static void SendMessage(long chat_id, string message, string reply_markup = "")
        {
            string BaseUrl = "https://api.telegram.org/bot";
            string token = "501412002:AAGLeNPhy3icrSfs4WbY0QhIRMwLULbmqDo";
            string address = BaseUrl + token + "/sendMessage";
            NameValueCollection nvc = new NameValueCollection();
            WebClient client = new WebClient();
            nvc.Add("chat_id", chat_id.ToString());
            nvc.Add("text", message);
            client.UploadValues(address, nvc);

        }
    }
   

}
