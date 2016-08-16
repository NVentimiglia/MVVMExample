using System;

namespace Lobby.Logic
{
    public enum NoticeColor
    {
        Log,
        Warning,
        Error,
        Success,
        Custon,

    }


    [Serializable]
    public class NoticeModel
    {
        public string Message;
        public NoticeColor Color;
    }
}