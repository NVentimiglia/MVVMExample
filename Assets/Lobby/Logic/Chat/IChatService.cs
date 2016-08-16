using System;

namespace Lobby.Logic
{
    public interface IChatService
    {
        event Action<ChatModel> OnChat;

        void Post(ChatModel model);
    }
}