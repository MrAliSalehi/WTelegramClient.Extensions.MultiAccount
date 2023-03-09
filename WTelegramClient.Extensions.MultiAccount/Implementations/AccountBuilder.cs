using WTelegram;
using WTelegramClient.Extensions.MultiAccount.Extensions;
using WTelegramClient.Extensions.MultiAccount.Interfaces;

namespace WTelegramClient.Extensions.MultiAccount.Implementations;

public class AccountBuilder : IAccountCreator
{
    private readonly Dictionary<string, Client> _clients;
    private AccountBuilder()
    {
        _clients = new Dictionary<string, Client>();
    }
    public static AccountBuilder CreateAccounts()
    {
        Common.DefaultLogger = Helpers.Log;
        Helpers.Log = (_, _) => { };
        return new();
    }


    public IAccountCreator FromPath(string path)
    {
        var client = new Client(null, new FileStream(path, FileMode.Open));
        if (client is null)
            throw new ArgumentNullException($"{nameof(client)} -cant be null");
        _clients.Add(new FileInfo(path).Name.Replace(".session", ""), client);

        return this;
    }

    public IAccountCreator FromAccount(TelAccount account, Func<string> verifyCode, string sessionName = "WTC.session")
    {
        CreateAccount(account, verifyCode, sessionName);
        return this;
    }

    public IAccountCreator FromAccount(Func<IApiHash, ITelAccountBuilder> builder, Func<string> verifyCode, string sessionName = "WTC.session")
    {
        var account = builder(TelAccountBuilder.New()).Build();
        CreateAccount(account, verifyCode, sessionName);
        return this;
    }

    public async ValueTask<IAccountHandler> BuildAsync()
    {
        foreach (var client in _clients)
        {
            await client.Value.LoginUserIfNeeded();
        }

        return new AccountHandler(_clients);
    }
    private void CreateAccount(TelAccount account, Func<string> verifyCode, string sessionName)
    {
        var client = new Client(str =>
        {
            return str switch
            {
                "api_id"            => account.ApiId,
                "api_hash"          => account.ApiHash,
                "phone_number"      => account.PhoneNumber,
                "verification_code" => verifyCode(),
                "first_name"        => account.FirstName,
                "last_name"         => account.LastName,
                "password"          => account.TwoFactor,
                _                   => null
            };
        }, new FileStream(sessionName, FileMode.OpenOrCreate));
        if (client is null)
            throw new ArgumentNullException($"{nameof(client)} cant be null");

        _clients.Add(sessionName.Replace(".session", ""), client);
    }
}