﻿using System;

namespace Bod.Models.Bot
{
    /// <summary>
    /// This object represents a Telegram user or bot.
    /// </summary>
    public class User
    {
        /// <summary>
        /// Unique identifier for this user, or bot, or group chat
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// User‘s or bot’s first name
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Optional. User‘s or bot’s last name
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Optional. User‘s or bot’s username
        /// </summary>
        public string Username { get; set; }
    }
}
