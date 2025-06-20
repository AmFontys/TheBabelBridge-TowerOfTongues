using BBTT.CrosswordModel;
using BBTT.DBModels;
using BBTT.DBModels.Crossword;
using BBTT.DBModels.User;
using Microsoft.EntityFrameworkCore;
using UserModel;

namespace BBTT.DBPostgres;

public class UserDataAcess : IUserDataAcess
{
    DbContextPostgres _pgsqlDbContext;

    public UserDataAcess (DbContextPostgres pgsqlDbContext)
    {
        _pgsqlDbContext = pgsqlDbContext;
    }

    public async Task<UserDTO> GetUser (int id)
    {
        var result = await _pgsqlDbContext.Users
            .Include(u => u.UserCrosswords)
            .ThenInclude(uc => uc.Crossword)
            .FirstOrDefaultAsync(u => u.Id == id);
        return result;
    }

    public async Task<UserDTO> GetUser (string name)
    {
        var result = await _pgsqlDbContext.Users
            .Include(u => u.UserCrosswords)
            .ThenInclude(uc => uc.Crossword)
            .FirstOrDefaultAsync(u => u.Name == name);
        return result;
    }

    public async Task<User> GetUser (string name, string password)
    {
        var result = await _pgsqlDbContext.Users
            .Include(u => u.UserCrosswords)
            .ThenInclude(uc => uc.Crossword)
            .FirstOrDefaultAsync(u => u.Email == name && u.Password == password);

        result = ObfuscateSecretInfo(result);

        User mappedUser = MapUserDtoToUser(result);

        return mappedUser;
    }

    private static User MapUserDtoToUser (UserDTO result)
    {
        return new User()
        {
            Name = result.Name,
            Password = result.Password,
            Email = result.Email,
            Role = (UserRoles)result.RoleId,
        };
    }

    public async Task<UserDTO> CreateUser(User user)
    {
        var userDto = new UserDTO()
        {
            Name = user.Name,
            Password = user.Password,
            Salt = user.Salt,
            Email = user.Email,
            UserCrosswords = new List<UserCrosswordDTO>(),
            CreatedAt = DateTime.UtcNow.ToString(),
            RoleId = (int)user.Role,
        };
        await _pgsqlDbContext.Users.AddAsync(userDto);
        await _pgsqlDbContext.SaveChangesAsync();

        userDto= ObfuscateSecretInfo(userDto);

        return userDto;
    }

    private static UserDTO ObfuscateSecretInfo (UserDTO userDto)
    {
        userDto.Salt = "";
        userDto.Password = "";
        userDto.Id = 0;
        userDto.RoleId = 0;
        return userDto;
    }

    public async Task<UserDTO> UpdateUser (UserDTO user)
    {
        var userDto = await _pgsqlDbContext.Users.FindAsync(user.Id);
        if (userDto == null)
            return null;
        userDto.Name = user.Name;
        userDto.Password = user.Password;
        userDto.Email = user.Email;
        await _pgsqlDbContext.SaveChangesAsync();
        return userDto;
    }

    public async Task<bool> DeleteUser (int id)
    {
        var userDto = await _pgsqlDbContext.Users.FindAsync(id);
        if (userDto == null)
            return false;
        //Anonymize user data instead of deleting it so the crosswords are still available
        userDto.Name = "Deleted User";
        userDto.Password = userDto.Password.Substring(0, 5) + "*****"; // Keep first 5 characters for reference

        await _pgsqlDbContext.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteUser (string name)
    {
        var userDto = await _pgsqlDbContext.Users.FirstOrDefaultAsync(u => u.Name == name);
        if (userDto == null)
            return false;
        _pgsqlDbContext.Users.Remove(userDto);
        await _pgsqlDbContext.SaveChangesAsync();
        return true;
    }

    public async Task<bool> AddCrosswordToUser (int userId, int crosswordId)
    {
        var user = await _pgsqlDbContext.Users
            .Include(u => u.UserCrosswords)
            .ThenInclude(uc => uc.Crossword)
            .FirstOrDefaultAsync(u => u.Id == userId);
        if (user == null)
            return false;
        var crossword = await _pgsqlDbContext.Crosswords.FindAsync(crosswordId);
        if (crossword == null)
            return false;
        user.UserCrosswords.Add(new UserCrosswordDTO()
        {
            UserId = userId,
            CrosswordId = crosswordId,
        });
        await _pgsqlDbContext.SaveChangesAsync();
        return true;
    }

    public async Task<List<UserDTO>> FindUsersByEmail(string email)
    {
        var result = await _pgsqlDbContext.Users            
            .Where(u => u.Email == email)
            .ToListAsync();
        return result;
    }

}
