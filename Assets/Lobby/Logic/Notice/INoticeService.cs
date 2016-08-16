using System;

namespace Lobby.Logic
{
    public interface INoticeService
    {
        event Action<NoticeModel> OnNotice;

        void PostLocal(NoticeModel model);
        void PostLocal(string message, NoticeColor color = NoticeColor.Log);
        void PostNetwork(NoticeModel model);
        void PostNetwork(string message, NoticeColor color = NoticeColor.Log);
    }
}