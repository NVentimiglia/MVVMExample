using System;
using Framework.Observables;

namespace Lobby.Logic
{
    public interface IScoreService
    {
        ScoreModel Self { get; }

        ObservableCollection<ScoreModel> Scores { get; }

        event Action<ScoreModel> OnScore;

        void Delete();
        void Post(ScoreModel model);
        void PostSelf();
    }
}