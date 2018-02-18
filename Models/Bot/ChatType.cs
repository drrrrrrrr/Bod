using System;

namespace Bod.Models.Bot
{
    public enum ChatType : byte
    {
        Unknown = 0,
        Private = 1,
        Group = 2,
        Supergroup = 3,
        Channel = 4
    }
}
