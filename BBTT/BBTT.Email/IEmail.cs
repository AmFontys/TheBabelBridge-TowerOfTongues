namespace BBTT.Email;

public interface IEmail
{
    Task CreateConnection();
    Task SendEmail (string email, string code);
}