using Framework;
using Lobby.Logic;
using UnityEngine;
using UnityEngine.UI;

namespace Lobby.Views
{
    [AddComponentMenu("Lobby/ChatView")]
    public class ChatView : ListViewBase<ChatModel, ChatItemView>
    {
        public GameObject Toggle;

        public GameObject PromptRoot;

        public InputField Prompt;

        [Inject]
        protected IAccountService Accounts;

        [Inject]
        protected IChatService Chats;

        public override void OnAwake()
        {
            base.OnAwake();
            Accounts.OnChange += state =>
            {
                if (state.IsAuthenticated)
                    Show();
                else
                    Hide();
            };

            //Bind to chat
            Chats.OnChat += Add;
        }

        public override void Bind(ChatItemView view, ChatModel model)
        {
            view.Label.text = string.Format("[{0}] {1}", model.Name, model.Message);
            //views self destroy
            view.DestroyCallback = Remove;
        }

        public void ShowPrompt()
        {
            PromptRoot.SetActive(true);
            Toggle.SetActive(false);
        }

        public void HidePrompt()
        {
            PromptRoot.SetActive(false);
            Toggle.SetActive(true);
        }

        public void Submit()
        {
            if (!string.IsNullOrEmpty(Prompt.text))
            {
                //TODO Pooling
                Chats.Post(new ChatModel
                {
                    Message = Prompt.text,
                    Name = Accounts.Account.UserName
                });
            }

            Prompt.text = string.Empty;
            HidePrompt();
        }
    }
}
