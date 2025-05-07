using BBTT.DBModels.User;
using System.Threading.Tasks;
using UserModel;

namespace BBTT.DBPostgres;

public interface IUserDataAcess
{
    Task<UserDTO> GetUser(int id);
    Task<UserDTO> GetUser(string email);
    Task<User> GetUser (string email, string password);
    Task<UserDTO> CreateUser (User user);

    Task<UserDTO> UpdateUser (UserDTO user);
    Task<bool> DeleteUser (int id);
    Task<bool> DeleteUser (string name);
    Task<bool> AddCrosswordToUser (int userId, int crosswordId);
    Task<List<UserDTO>> FindUsersByEmail (string email);

}