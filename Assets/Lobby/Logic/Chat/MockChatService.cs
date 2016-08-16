using System;

namespace Lobby.Logic
{
    public class MockChatService : IChatService
    {
        public event Action<ChatModel> OnChat;

        public void Post(ChatModel model)
        {
            OnChat(model);
        }
        
    }
}
