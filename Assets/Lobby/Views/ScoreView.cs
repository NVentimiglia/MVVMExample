using Framework;
using Lobby.Logic;
using UnityEngine;

namespace Lobby.Views
{
    [AddComponentMenu("Lobby/ScoreView")]
    public class ScoreView : ListViewBase<ScoreModel, ScoreViewItem>
    {
        [Inject] protected IScoreService Scores;
        [Inject] protected ConfirmView ConfirmView;

        public override void OnAwake()
        {
            base.OnAwake();
            //Collection Binding
            Subscribe(Scores.Scores);

            //Notice Binding
            Scores.OnScore += model =>
            {
                Notice(string.Format("{0} has {1} points", model.UserName, model.Score));
            };
        }

        public override void Bind(ScoreViewItem view, ScoreModel model)
        {
            view.NameLabel.text = model.UserName;
            view.ScoreLabel.text = model.Score.ToString();
            view.FlagImage.gameObject.SetActive(model.IsSelf);
        }

        public void Refresh()
        {
            ConfirmView.Callback = DoExit;
            ConfirmView.Title.text = "Quit ?";
            ConfirmView.Label.text = "All progress will be lost.";
            ConfirmView.Show();
        }

        void DoExit()
        {
            LobbyController.Instance.Reset();
        }

        public void Back()
        {
            Hide();
        }
    }
}
