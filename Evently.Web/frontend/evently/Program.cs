var builder = WebApplication.CreateBuilder(args);
var port = Environment.GetEnvironmentVariable("PORT") ?? "3000";
builder.WebHost.UseUrls($"http://0.0.0.0:{port}");
builder.Services.AddRazorPages();

var app = builder.Build();
app.UseDefaultFiles();
app.UseStaticFiles();
app.MapGet("/healthz", () => Results.Ok("healthy"));
app.MapRazorPages();
app.Run();
