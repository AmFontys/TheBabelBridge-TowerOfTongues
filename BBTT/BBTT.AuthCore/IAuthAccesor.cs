using UserModel;

namespace BBTT.AuthCore;

public interface IAuthAccesor
{
    Task<User> Login (string email, string password);
    Task<User> Register (string name, string email, string password);
    Task<bool> Logout ();
    Task<bool> ChangePassword (string email, string oldPassword, string newPassword);
    Task<bool> ResetPassword (string email);
    Task<bool> VerifyEmail (string email);
    Task<bool> GetUserInfo (string email);
    string SaltPassword (string password);
}