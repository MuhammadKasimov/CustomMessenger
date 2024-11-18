using CustomMessengerBlazor.Components;
using CustomMessengerBlazor.Helpers;
using CustomMessengerBlazor.Interfaces;
using CustomMessengerBlazor.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IGroupService, GroupService>();
builder.Services.AddScoped<IChatService, ChatService>();
builder.Services.AddScoped<IMessageService, MessageService>();


var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

ApiSettings.URI = app.Configuration.GetValue<string>("CustomMessengerAPI");
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
