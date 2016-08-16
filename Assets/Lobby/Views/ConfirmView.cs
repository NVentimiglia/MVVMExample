using System;
using UnityEngine;
using UnityEngine.UI;

namespace Lobby.Views
{
    [AddComponentMenu("Lobby/ConfirmView")]
    public class ConfirmView : ViewBase
    {
        public Text Title;

        public Text Label;

        public Action Callback;

        public void Cancel()
        {
            Hide();
            Callback = null;
        }

        public void Continue()
        {
            if (Callback != null)
            {
                Callback();
                Callback = null;
            }

            Hide();
        }

    }
}
