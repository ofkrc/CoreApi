using CoreApi.Core.Base.Model;
using CoreApi.Models;
using CoreApi.Models.Request.User;
using CoreApi.Models.Response.User;
using CoreApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class UserController : BaseController
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost("Register")]
    public ActionResult<BaseResponse<User>> Register([FromBody] UserRequestModel request)
    {
        try
        {
            var newUser = _userService.Register(request);
            return SuccessResponse(newUser, "Kullanıcı başarıyla kaydedildi.");
        }
        catch (Exception ex)
        {
            return ErrorResponse<User>(ex.Message);
        }
    }

    [HttpPost("Login")]
    public async Task<ActionResult<BaseResponse<UserLoginResponse>>> Login([FromBody] UserLoginRequestModel request)
    {
        try
        {
            var loginUser = await _userService.LoginUserAsync(request);
            return SuccessResponse(loginUser, "Giriş başarılı.");
        }
        catch (Exception ex)
        {
            return ErrorResponse<UserLoginResponse>(ex.Message);
        }
    }

    [HttpGet("Search")]
    public ActionResult<BaseResponse<List<User>>> SearchUsers([FromQuery] string searchTerm)
    {
        try
        {
            var users = _userService.SearchUsers(searchTerm);
            return SuccessResponse(users, "Kullanıcılar başarıyla bulundu.");
        }
        catch (Exception ex)
        {
            return ErrorResponse<List<User>>(ex.Message);
        }
    }

    [HttpGet("Get")]
    public ActionResult<BaseResponse<List<User>>> Get()
    {
        try
        {
            var users = _userService.GetAllUsers();
            return SuccessResponse(users, "Kullanıcılar başarıyla getirildi.");
        }
        catch (Exception ex)
        {
            return ErrorResponse<List<User>>(ex.Message);
        }
    }

    [HttpGet("Get/{userId}")]
    [NonAction]
    public ActionResult<BaseResponse<User>> GetUserById(int recordId)
    {
        try
        {
            var user = _userService.GetUserById(recordId);

            if (user == null)
            {
                return ErrorResponse<User>("Kullanıcı bulunamadı.");
            }

            return SuccessResponse(user, "Kullanıcı başarıyla getirildi.");
        }
        catch (Exception ex)
        {
            return ErrorResponse<User>(ex.Message);
        }
    }

    [HttpPost("Update")]
    public ActionResult<BaseResponse<User>> Update([FromBody] UserRequestModel request)
    {
        try
        {
            var updatedUser = _userService.Update(request);

            if (updatedUser == null)
            {
                return ErrorResponse<User>("Kullanıcı bulunamadı.");
            }

            return SuccessResponse(updatedUser, "Kullanıcı başarıyla güncellendi.");
        }
        catch (Exception ex)
        {
            return ErrorResponse<User>(ex.Message);
        }
    }

    [HttpDelete("Delete/{recordId}")]
    public ActionResult<BaseResponse<string>> Delete(int recordId)
    {
        try
        {
            var user = _userService.GetUserById(recordId);
            if (user == null)
            {
                return ErrorResponse<string>("Kullanıcı bulunamadı.");
            }

            _userService.Delete(user);
            return SuccessResponse<string>("Kullanıcı başarıyla silindi.");
        }
        catch (Exception ex)
        {
            return ErrorResponse<string>(ex.Message);
        }
    }
}
