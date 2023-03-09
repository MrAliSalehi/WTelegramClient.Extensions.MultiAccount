using WTelegram;
using WTelegramClient.Extensions.MultiAccount.Extensions;
using WTelegramClient.Extensions.MultiAccount.Interfaces;

namespace WTelegramClient.Extensions.MultiAccount.Implementations;

public class AccountHandler : IAccountHandler
{
    private readonly Dictionary<string, Client> _clients;
    private bool _withParallel;
    private TimeSpan _delay = TimeSpan.MinValue;
    Dictionary<string, Client> IAccountHandler.Clients
    {
        get => _clients;
    }
    internal AccountHandler(Dictionary<string, Client> clients)
    {
        _clients = clients;
        Helpers.Log = Common.DefaultLogger;
    }

    public IReadOnlyList<Client> GetClients()
    {
        return _clients.Values.ToList();
    }
    public IAccountHandler WithDelay(TimeSpan delay)
    {
        _delay = _delay.Add(delay);
        return this;
    }

    public IAccountHandler OnParallel()
    {
        _withParallel = true;
        return this;
    }
    public async ValueTask<IAccountHandler> ForEachClientAsync(Func<Client, string, Task> onEachClient)
    {
        if (_withParallel)
        {
            await Parallel.ForEachAsync(_clients, async (keyValue, ct) =>
            {
                await Task.Delay(_delay, ct);
                await onEachClient(keyValue.Value, keyValue.Key);
            });
        }
        else
        {
            foreach (var (name, client) in _clients)
            {
                await Task.Delay(_delay);
                await onEachClient(client, name);
            }
        }

        return this;
    }
}