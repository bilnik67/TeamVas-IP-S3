namespace TeamVas.API.Middleware.Chat
{
    public interface IChatClient
    {
        Task ReceiveMessage(MessageModel message);
    }
}
