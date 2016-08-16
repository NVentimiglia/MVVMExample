using System;
using UnityEngine;

namespace Lobby.Logic
{
    /// <summary>
    /// Data Manager for Notices
    /// </summary>
    public class MockNoticeService : INoticeService
    {
        /// <summary>
        /// Incoming notice
        /// </summary>
        public event Action<NoticeModel> OnNotice = delegate { };
   
        /// <summary>
        /// Posts a notice
        /// </summary>
        public void PostLocal(string message, NoticeColor color = NoticeColor.Log)
        {
            PostLocal(new NoticeModel { Color = color, Message = message});
        }

        /// <summary>
        /// Posts a notice
        /// </summary>
        public void PostLocal(NoticeModel model)
        {
            Handle(model);
        }

        /// <summary>
        /// Posts a notice
        /// </summary>
        public void PostNetwork(string message, NoticeColor color = NoticeColor.Log)
        {
            PostNetwork(new NoticeModel { Color = color, Message = message });
        }

        /// <summary>
        /// Posts a notice
        /// </summary>
        public void PostNetwork(NoticeModel model)
        {
            Handle(model);
        }

        void Handle(NoticeModel model)
        {
            Debug.Log(model.Message);
            OnNotice(model);
        }
    }
}
