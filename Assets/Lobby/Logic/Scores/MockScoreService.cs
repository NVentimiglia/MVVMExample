using System;
using Framework;
using Framework.Observables;

namespace Lobby.Logic
{

    public class MockScoreService : IScoreService
    {
        /// <summary>
        /// High scores collection
        /// </summary>
        public ObservableCollection<ScoreModel> Scores { get; set; }

        /// <summary>
        /// Incoming score notice
        /// </summary>
        public event Action<ScoreModel> OnScore = delegate { };
        
        /// <summary>
        /// Self
        /// </summary>
        public ScoreModel Self { get; private set; }

        private IAccountService _accounts;
        public IAccountService Accounts
        {
            get { return _accounts ?? (_accounts = InjectController.Get<IAccountService>()); }
        }

        private INoticeService _notices;
        public INoticeService Notices
        {
            get { return _notices ?? (_notices = InjectController.Get<INoticeService>()); }
        }


        public MockScoreService()
        {
            //data
            Scores = new ObservableCollection<ScoreModel>();
            Scores.Comperer = new ScoreComparer();
            Self = new ScoreModel();

        }

        /// <summary>
        /// Post self change
        /// </summary>
        public void PostSelf()
        {
            Post(Self);
        }

        /// <summary>
        /// Posts a score
        /// </summary>
        public void Post(ScoreModel model)
        {
            HandleScore(model);
        }

        /// <summary>
        /// Resets account to a new instance
        /// </summary>
        public void Delete()
        {
            Self.Score = 0;
            Scores.Clear();
        }


        void HandleScore(ScoreModel model)
        {
            //setting
            model.IsSelf = Accounts.Account.Id == model.AccountId;

            //managed collection
            Scores.AddOrReplace(model);

            //general input notice
            OnScore(model);

            //Print
            Notices.PostNetwork(string.Format("{0} has a score of {1}", model.UserName, model.Score));
        }
    }
}
