using System;

namespace Bod.Models.Bot
{
    /// <summary>
    /// This object represents an incoming callback query from a callback button in an inline keyboard. 
    /// If the button that originated the query was attached to a message sent by the bot, the field message will be presented. 
    /// If the button was attached to a message sent via the bot (in inline mode), the field inline_message_id will be presented.
    /// </summary>
    public class CallbackQuery
    {
        /// <summary>
        /// Unique identifier for this query
        /// </summary>
        public string id { get; set; }

        /// <summary>
        /// Sender
        /// </summary>
        public User from { get; set; }

        /// <summary>
        /// Optional. Message with the callback button that originated the query. 
        /// Note that message content and message date will not be available if the message is too old
        /// </summary>
        public Message message { get; set; }

        /// <summary>
        /// Optional. Identifier of the message sent via the bot in inline mode, that originated the query
        /// </summary>
        public string inline_message_id { get; set; }

        /// <summary>
        /// Data associated with the callback button. Be aware that a bad client can send arbitrary data in this field
        /// </summary>
        public string data { get; set; }
    }
}
