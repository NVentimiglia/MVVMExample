using System;
using Framework;
using Lobby.Logic;
using UnityEngine;
using UnityEngine.UI;

namespace Lobby.Views
{
    [AddComponentMenu("Lobby/ErrorView")]
    public class ErrorView : ViewBase
    {
        public string Message
        {
            get { return Label.text; }
            set { Label.text = value; }
        }

        public Text Label;

        public Action Callback;

        [Inject]
        protected IErrorService Errors;

        public override void OnAwake()
        {
            base.OnAwake();
            Errors.OnError += (ex, cb) =>
            {
                Message = ex.Message;
                Callback = cb;
                Show();
            };

            base.OnAwake();
        }

        public void Continue()
        {
            // TODO, Errors should really have a service with logging.
            // This view should listen to this service
            Hide();

            if (Callback != null)
            {
                Callback();
                Callback = null;
            }
        }

    }
}
