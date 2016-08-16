using UnityEngine;
using UnityEngine.UI;

namespace Lobby.Views
{
    [AddComponentMenu("Lobby/ScoreViewItem")]
    public class ScoreViewItem : MonoBehaviour
    {
        public Text NameLabel;
        public Text ScoreLabel;
        public Image FlagImage;
    }
}
