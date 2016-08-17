using System;

namespace Lobby.Logic
{
    public interface IAccountService
    {
        AccountModel Account { get; }

        event Action<AccountModel> OnChange;
        
        void Delete();
        void Load();
        void Save();
        
        void RaiseChange();
    }
}
