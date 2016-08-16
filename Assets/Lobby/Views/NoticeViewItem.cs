using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Lobby.Views
{
    [AddComponentMenu("Lobby/NoticeViewItem")]
    public class NoticeViewItem : MonoBehaviour
    {
        public Text Label;
        public float ShowTime = 5;
        public Action<NoticeViewItem> DestroyCallback;

        IEnumerator Start()
        {
            yield return new WaitForSeconds(ShowTime);

            Destroy(gameObject);
        }

        void OnDestroy()
        {
            if (DestroyCallback != null)
            {
                DestroyCallback(this);
                DestroyCallback = null;
            }
        }
    }
}
