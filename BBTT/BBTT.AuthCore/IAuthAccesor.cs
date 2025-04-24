namespace BBTT.AuthCore;

public interface IAuthAccesor
{
    Task<bool> Login (string email, string password);
    Task<bool> Register (string name, string email, string password);
    Task<bool> Logout ();
    Task<bool> ChangePassword (string email, string oldPassword, string newPassword);
    Task<bool> ResetPassword (string email);
    Task<bool> VerifyEmail (string email);
    Task<bool> GetUserInfo (string email);
}