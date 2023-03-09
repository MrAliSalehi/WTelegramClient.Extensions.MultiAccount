namespace WTelegramClient.Extensions.MultiAccount.Interfaces;

public interface IApiHash
{
    IApiId WithHash(string hash);
}