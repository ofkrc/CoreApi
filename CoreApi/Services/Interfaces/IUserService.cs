using EmaAPI.Models.Request.User;
using EmaAPI.Models.Response.User;
using EmaAPI.Models;

namespace EmaAPI.Services.Interfaces
{
    public interface IUserService
    {
        User Register(UserRequestModel request);
        List<User> SearchUsers(string searchTerm);
        List<User> GetAllUsers();
        User GetUserById(int recordId);
        User Update(UserRequestModel request);
        User Delete(User user);
        Task<UserLoginResponse> LoginUserAsync(UserLoginRequestModel request);
    }
}
