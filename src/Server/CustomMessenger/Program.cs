using CustomMessenger.Extensions;
using CustomMessenger.Hubs;
using CustomMessenger.MIddlewares;
using CustomMessenger.Service.Helpers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddCustomServices(builder.Configuration);
builder.Services.AddJwtService(builder.Configuration);
builder.Services.AddSwaggerService();
builder.Services.AddLoggerService();
builder.Services.AddCorsService();
builder.Services.AddHttpContextAccessor();
builder.Services.AddSignalR();
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
if (app.Services.GetService<IHttpContextAccessor>() != null)
{
    HttpContextHelper.Accessor = app.Services.GetRequiredService<IHttpContextAccessor>();
}



EnvironmentHelper.WebRootPath = app.Services.GetRequiredService<IWebHostEnvironment>().WebRootPath;
app.UseMiddleware<ExceptionHandlerMiddleware>();
app.UseCors("AllowAll");
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.MapHub<ChatHub>("/chathub");
app.Run();
