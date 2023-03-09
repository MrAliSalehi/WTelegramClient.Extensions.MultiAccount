using WTelegram;
using WTelegramClient.Extensions.MultiAccount.Interfaces;

namespace WTelegramClient.Extensions.MultiAccount.Extensions;

public static class AccountHandlerExtensions
{
    public static Client? ByName(this IAccountHandler accountHandler, string name)
    {
        accountHandler.Clients.TryGetValue(name, out var client);
        return client;
    }
}