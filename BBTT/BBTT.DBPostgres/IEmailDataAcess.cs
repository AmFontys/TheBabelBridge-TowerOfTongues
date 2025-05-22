namespace BBTT.DBPostgres;

public interface IEmailDataAcess
{
    Task SaveEmailCodeAsync (string email, string code);
    Task<bool> VerifyEmailCodeAsync (string email, string code);

}