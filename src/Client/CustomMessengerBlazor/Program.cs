using CustomMessengerBlazor.Components;
using CustomMessengerBlazor.Helpers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

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
