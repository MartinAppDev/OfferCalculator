using VueCliMiddleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddSpaStaticFiles(configuration =>
{
    configuration.RootPath = "frontend/";
});

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseRouting();
app.UseStaticFiles();
app.UseSpaStaticFiles();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller}/{action=Index}/{id?}");
});

app.UseSpa(spa =>
{
    if (app.Environment.IsDevelopment())
    {
        spa.Options.SourcePath = "frontend/";
        spa.UseVueCli(npmScript: "serve");
    } else
    {
        spa.Options.SourcePath = "dist";
    }
    
});

app.Run();

