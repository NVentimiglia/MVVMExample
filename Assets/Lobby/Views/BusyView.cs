using UnityEngine;
using UnityEngine.UI;

namespace Lobby.Views
{
    [AddComponentMenu("Lobby/BusyView")]
    public class BusyView : ViewBase
    {
        public string Message
        {
            get { return Label.text; }
            set { Label.text = value; }
        }

        public Text Label;

        public void Retry()
        {

        }
    }
}
