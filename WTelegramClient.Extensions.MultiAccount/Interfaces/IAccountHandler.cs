using WTelegram;

namespace WTelegramClient.Extensions.MultiAccount.Interfaces;

public interface IAccountHandler
{
    internal Dictionary<string, Client> Clients { get; }
    public IReadOnlyList<Client> GetClients();
    public ValueTask<IAccountHandler> ForEachClientAsync(Func<Client, string, Task> onEachClient);
    public IAccountHandler WithDelay(TimeSpan delay);
    public IAccountHandler OnParallel();
}