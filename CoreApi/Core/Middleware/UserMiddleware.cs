namespace CoreApi.Core.Middleware
{
    public class UserIdMiddleware
    {
        private readonly RequestDelegate _next;

        public UserIdMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (context.User.Identity.IsAuthenticated)
            {
                // Token'dan UserId'yi alın
                var userIdClaim = context.User.FindFirst("UserId");
                if (userIdClaim != null && int.TryParse(userIdClaim.Value, out var userId))
                {
                    // UserId'yi HttpContext.Items'e veya başka bir yere kaydedin
                    context.Items["UserId"] = userId;
                }
            }

            await _next(context);
        }
    }

}
