namespace UserModel;

public class User
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string Salt { get; set; }
    public UserRoles Role { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public override string ToString ()
    {
        return $"Name: {Name}, Email: {Email}, Password: {Password}, Role: {Role}, CreatedAt: {CreatedAt}";
    }
}


public class LoginModel
{
    public string Email { get; set; }
    public string Password { get; set; }
}

public class RegisterModel
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
}

public class ChangePasswordModel
{
    public string Email { get; set; }
    public string OldPassword { get; set; }
    public string NewPassword { get; set; }
}

public class ResetPasswordModel
{
    public string Email { get; set; }
    public string NewPassword { get; set; }
    public string ResetToken { get; set; }
}

public class VerifyEmailModel
{
    public string Email { get; set; }
    public string VerificationToken { get; set; }
}