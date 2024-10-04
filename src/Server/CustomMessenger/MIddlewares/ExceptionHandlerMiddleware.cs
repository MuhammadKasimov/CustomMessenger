using CustomMessenger.Service.Exceptions;
using CustomMessenger.Service.Helpers;
using CustomMessenger.Service.Interfaces;
using Newtonsoft.Json;

namespace CustomMessenger.MIddlewares
{
    public class ExceptionHandlerMiddleware(RequestDelegate next, ILogger<HttpStatusCodeException> logger, IUserService userService)
    {
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                if (context.User.HasClaim(c => c.Type == "Id"))
                {
                    if (HttpContextHelper.UserId is null)
                        throw new HttpStatusCodeException(401, "Unathorized");

                    var user = await userService.GetByIdAsync((Guid)HttpContextHelper.UserId);

                    if (user is null)
                        throw new HttpStatusCodeException(401, "Unathorized");
                }
                await next.Invoke(context);
            }
            catch (HttpStatusCodeException ex)
            {
                logger.LogWarning(ex.Message + $"{ex.Code}");

                await HandleException(context, ex.Code, ex.Message);

            }
            catch (Exception ex)
            {
                logger.LogError(ex.ToString());

                await HandleException(context, 500, ex.Message);
            }
        }

        public async Task HandleException(HttpContext context, int code, string message)
        {
            context.Response.StatusCode = code;
            var response = new
            {
                Code = code,
                Message = message
            };
            await context.Response.WriteAsJsonAsync(response);
            logger.LogWarning($"{JsonConvert.SerializeObject(response)}");
        }
    }
}
