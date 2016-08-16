using UnityEngine;
using UnityEngine.UI;

namespace Lobby.Views
{
    [AddComponentMenu("Lobby/OptionView")]
    public class OptionView : ViewBase
    {
        private string[] Transports = new string[]
        {
            "Websockets",
            "Udp",
            "Tcp",
        };

        public int TransportValue;


        public Slider Sfx;
        public Slider Music;
        public Text TransportLabel;

        public GameObject WindowRoot;

        public override void OnAwake()
        {
            base.OnAwake();
            Load();
        }

        protected override void OnHide()
        {
            Save();
        }
        
        public void ShowHelp()
        {
            LobbyController.Instance.HelpView.Show();
        }
        
        public void ToggleTransport()
        {
            if (TransportValue == 0)
            {
                TransportValue = Transports.Length - 1;
            }
            else
            {
                TransportValue--;
            }
            RenderTransport();
        }

        public void SignOut()
        {
            LobbyController.Instance.Reset();
        }

        public void ShowWebsite()
        {
            Application.OpenURL("http://nicholasventimiglia.com");
        }

        void RenderTransport()
        {
            TransportLabel.text = Transports[TransportValue];
        }

        public void Load()
        {
            Sfx.value = PlayerPrefs.GetFloat("Sfx", 1);
            Music.value = PlayerPrefs.GetFloat("Music", 1);
            TransportValue = PlayerPrefs.GetInt("Transport", 0);
            RenderTransport();
        }

        public void Save()
        {
            PlayerPrefs.SetFloat("Sfx", Sfx.value);
            PlayerPrefs.SetFloat("Music", Music.value);
            PlayerPrefs.SetInt("Transport", TransportValue);
            PlayerPrefs.Save();
        }
    }
}
