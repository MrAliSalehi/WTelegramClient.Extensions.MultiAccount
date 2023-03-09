using WTelegramClient.Extensions.MultiAccount.Interfaces;

namespace WTelegramClient.Extensions.MultiAccount.Implementations;

public sealed class TelAccountBuilder : IApiHash, IApiId, IPhoneNumber, IOptionalInfo
{
    private readonly TelAccount _account;
    private TelAccountBuilder(TelAccount account)
    {
        _account = account;
    }
    public static IApiHash New() => new TelAccountBuilder(TelAccount.Empty);

    public TelAccount Build() => _account;
    public IApiId WithHash(string hash)
    {
        ArgumentException.ThrowIfNullOrEmpty(hash);
        _account.ApiHash = hash;
        return this;
    }
    public IPhoneNumber WithApiId(string id)
    {
        ArgumentException.ThrowIfNullOrEmpty(id);
        _account.ApiId = id;
        return this;
    }
    public IOptionalInfo WithPhone(string phone)
    {
        ArgumentException.ThrowIfNullOrEmpty(phone);
        _account.PhoneNumber = phone;
        return this;
    }

    public IOptionalInfo WithFirstName(string firstName)
    {
        ArgumentException.ThrowIfNullOrEmpty(firstName);
        _account.FirstName = firstName;
        return this;
    }
    public IOptionalInfo WithLastName(string lastName)
    {
        ArgumentException.ThrowIfNullOrEmpty(lastName);
        _account.LastName = lastName;
        return this;
    }
    public IOptionalInfo WithTwoFactorAuth(string password)
    {
        ArgumentException.ThrowIfNullOrEmpty(password);
        _account.TwoFactor = password;
        return this;
    }
}