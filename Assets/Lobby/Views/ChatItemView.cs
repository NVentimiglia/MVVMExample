using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Lobby.Views
{
    [AddComponentMenu("Lobby/ChatItemView")]
    public class ChatItemView : MonoBehaviour
    {
        public Text Label;
        public float ShowTime = 5;
        public Action<ChatItemView> DestroyCallback;

       
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
