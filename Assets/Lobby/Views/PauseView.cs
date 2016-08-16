using Framework;
using Lobby.Logic;
using UnityEngine;

namespace Lobby.Views
{
    [AddComponentMenu("Lobby/PauseView")]
    public class PauseView : ViewBase
    {
        [Inject]
        protected IAccountService Accounts;

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
        }

        public void ShowScores()
        {
            InjectController.Get<ScoreView>().Show();
        }
    }
}
