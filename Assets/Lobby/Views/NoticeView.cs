using Framework;
using Lobby.Logic;
using UnityEngine;

namespace Lobby.Views
{
    [AddComponentMenu("Lobby/NoticeView")]
    public class NoticeView : ListViewBase<NoticeModel, NoticeViewItem>
    {
        [SerializeField]
        Color NormalColor = Color.white;
        [SerializeField]
        Color WarningColor = Color.yellow;
        [SerializeField]
        Color ErrorColor = Color.red;
        [SerializeField]
        Color SuccessColor = Color.green;

        [Inject]
        private INoticeService Notices;

        public override void OnAwake()
        {
            base.OnAwake();
            Notices.OnNotice += Add;
        }

        public override void Bind(NoticeViewItem view, NoticeModel model)
        {
            //views self destroy
            view.DestroyCallback = Remove;
          
            //text
            view.Label.text = model.Message;

            //paint it
            switch (model.Color)
            {
                case NoticeColor.Log:
                    view.Label.color = NormalColor;
                    break;
                case NoticeColor.Warning:
                    view.Label.color = WarningColor;
                    break;
                case NoticeColor.Error:
                    view.Label.color = ErrorColor;
                    break;
                case NoticeColor.Success:
                    view.Label.color = SuccessColor;
                    break;
            }
        }
    }
}
