namespace WTelegramClient.Extensions.MultiAccount.Interfaces;

public interface IOptionalInfo : ITelAccountBuilder
{
    IOptionalInfo WithFirstName(string firstName);
    IOptionalInfo WithLastName(string lastName);
    IOptionalInfo WithTwoFactorAuth(string password);
}