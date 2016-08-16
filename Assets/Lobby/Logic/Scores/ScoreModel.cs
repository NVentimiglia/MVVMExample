using System;
using System.Collections.Generic;

namespace Lobby.Logic
{
    [Serializable]
    public class ScoreModel : IEquatable<ScoreModel>, IComparable<ScoreModel>
    {
        public string AccountId = string.Empty;
        public string UserName = string.Empty;
        public int Score;
        public bool IsSelf;

        public int CompareTo(ScoreModel other)
        {
            return Score - other.Score;
        }


        public bool Equals(ScoreModel other)
        {
            return other != null && other.AccountId == AccountId;
        }

        public override int GetHashCode()
        {
            return AccountId.GetHashCode();
        }
    }

    public class ScoreComparer : IComparer<ScoreModel>
    {
        public int Compare(ScoreModel x, ScoreModel y)
        {
            return x.CompareTo(y);
        }
    }
}