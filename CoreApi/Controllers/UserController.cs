using EmaAPI.Models;
using EmaAPI.Models.Request.User;
using EmaAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
	private readonly IUserService _userService;

	public UserController(IUserService userService)
	{
		_userService = userService;
	}

	[HttpPost("Register")]
	public ActionResult<User> Register([FromBody] UserRequestModel request)
	{
		var newUser = _userService.Register(request);

		return CreatedAtAction(nameof(Register), new { id = newUser.RecordId }, newUser);
	}

	[HttpPost("Login")]
	public ActionResult<User> Login([FromBody] UserLoginRequestModel request)
	{
		var loginUser = _userService.LoginUserAsync(request);

		return Ok(loginUser);
	}

	[HttpGet("Search")]
	public ActionResult<List<User>> SearchUsers([FromQuery] string searchTerm)
	{
		var users = _userService.SearchUsers(searchTerm);
		return Ok(users);
	}

	[HttpGet("Get")]
	public IActionResult Get()
	{
		var users = _userService.GetAllUsers();
		return Ok(users);
	}

	[HttpGet("Get/{userId}")]
	[NonAction]
	public IActionResult GetUserById(int recordId)
	{
		var user = _userService.GetUserById(recordId);

		if (user == null)
		{
			return NotFound();
		}

		return Ok(user);
	}

	[HttpPost("Update")]
	public IActionResult Update([FromBody] UserRequestModel request)
	{
		try
		{
			// Güncellenmek istenen kullanıcının kaydını al
			var updatedUser = _userService.Update(request);

			// Kullanıcı bulunamazsa 404 Not Found döndür
			if (updatedUser == null)
			{
				return NotFound();
			}

			return Ok(updatedUser);
		}
		catch (Exception ex)
		{
			return StatusCode(500, $"Internal Server Error: {ex.Message}");
		}
	}


	[HttpDelete("Delete/{recordId}")]
	public IActionResult Delete(int recordId)
	{
		var user = _userService.GetUserById(recordId);
		if (user == null)
		{
			return NotFound();
		}

		_userService.Delete(user);
		return NoContent();
	}
}
