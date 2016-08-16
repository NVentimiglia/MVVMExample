using System;

namespace Lobby.Logic
{
    public interface IErrorService
    {
        event Action<Exception, Action> OnError;

        void Post(Exception model, Action callback = null);
    }
}