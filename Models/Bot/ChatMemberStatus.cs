using System;

namespace Bod.Models.Bot
{
    public enum ChatMemberStatus : byte
    {
        Unknown,
        Creator,
        Administrator,
        Member,
        Left,
        Kicked
    }
}
