using CoreApi.Models.Request.User;
using CoreApi.Models.Response.User;
using CoreApi.Models;

namespace CoreApi.Services.Interfaces
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
