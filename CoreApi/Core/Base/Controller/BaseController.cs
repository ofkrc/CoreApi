using CoreApi.Core.Base.Model;
using Microsoft.AspNetCore.Mvc;

[ApiController]
public abstract class BaseController : ControllerBase
{
    protected ActionResult<BaseResponse<T>> SuccessResponse<T>(T data, string message = "")
    {
        var response = new BaseResponse<T>(data, true, message);
        return Ok(response);
    }

    protected ActionResult<BaseResponse<T>> ErrorResponse<T>(string message, T data = default)
    {
        var response = new BaseResponse<T>(data, false, message);
        return BadRequest(response);
    }
}
