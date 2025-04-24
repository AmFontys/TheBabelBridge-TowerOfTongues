namespace UserModel;

public class User
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public UserRoles Role { get; set; }
    public DateTime CreatedAt { get; set; }

    public override string ToString ()
    {
        return $"Name: {Name}, Email: {Email}, Password: {Password}, Role: {Role}, CreatedAt: {CreatedAt}";
    }
}
