using Framework;
using Lobby.Logic;
using UnityEngine;
using UnityEngine.UI;

namespace Lobby.Views
{
    [AddComponentMenu("Lobby/DemoGameView")]
    public class DemoGameView : ViewBase
    {
        public Text Title;

        [Inject]
        protected IAccountService Accounts;

        [Inject]
        protected IScoreService Scores;

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

        protected override void OnShow()
        {
            base.OnShow();

            //This should really not be here.
            Scores.Self.UserName = Accounts.Account.UserName;
        }

        public void DoScore()
        {
            Scores.Self.Score++;
            Scores.PostSelf();
        }
    }
}
