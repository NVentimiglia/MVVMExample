using System;

namespace Lobby.Logic
{
    [Serializable]
    public class AccountModel
    {
        public string Id;
        public string UserName;
        public bool IsAuthenticated;
    }
}