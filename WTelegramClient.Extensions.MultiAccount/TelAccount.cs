namespace WTelegramClient.Extensions.MultiAccount;

public sealed class TelAccount
{
    public static TelAccount New(string apiId,
                                 string apiHash,
                                 string phoneNumber,
                                 string? twoFactor = null,
                                 string? firstName = null,
                                 string? lastName = null) => new()
    {
        ApiId = apiId,
        ApiHash = apiHash,
        PhoneNumber = phoneNumber,
        FirstName = firstName,
        LastName = lastName,
        TwoFactor = twoFactor
    };
    public static readonly TelAccount Empty = New("", "", "");
    private TelAccount() { }
    public string ApiId { get; set; } = default!;
    public string ApiHash { get; set; } = default!;
    public string PhoneNumber { get; set; } = default!;
    public string? TwoFactor { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
}