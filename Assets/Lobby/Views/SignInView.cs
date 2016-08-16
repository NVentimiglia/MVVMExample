using Framework;
using Lobby.Logic;
using UnityEngine;
using UnityEngine.UI;

namespace Lobby.Views
{
    [AddComponentMenu("Lobby/SignInView")]
    public class SignInView : ViewBase
    {
        public InputField Prompt;

        [Inject] protected IAccountService Accounts;
        
        protected override void OnShow()
        {
            Prompt.text = Accounts.Account.UserName;
        }

        public void Continue()
        {
            if (string.IsNullOrEmpty(Prompt.text))
            {
                ShowError("A man must have a name.");
                return;
            }

            Accounts.Account.UserName = Prompt.text;
            Accounts.Account.IsAuthenticated = transform;
            Accounts.Save();

            Hide();
        }
    }
}
