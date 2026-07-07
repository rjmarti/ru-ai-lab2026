using Microsoft.EntityFrameworkCore;
using SsoAdmin.Application.Interfaces;
using SsoAdmin.Application.Services;
using SsoAdmin.Data;
using SsoAdmin.Models;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages(options =>
{
    options.Conventions.AuthorizeFolder("/");
    options.Conventions.AllowAnonymousToPage("/Login");
});

builder.Services.AddAuthentication("Cookies")
    .AddCookie("Cookies", options =>
    {
        options.LoginPath = "/Login";
        options.LogoutPath = "/Logout";
        options.AccessDeniedPath = "/Login";
        options.ExpireTimeSpan = TimeSpan.FromHours(8);
    });

builder.Services.AddDbContext<SsoAdminDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("Default")));

builder.Services.AddScoped<ILoginService, LoginService>();
builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddScoped<ICredencialService, CredencialService>();
builder.Services.AddScoped<IAplicacionService, AplicacionService>();
builder.Services.AddScoped<IPermisoService, PermisoService>();

WebApplication app = builder.Build();

await SeedAsync(app);

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();
app.MapRazorPages().WithStaticAssets();

app.Run();

static async Task SeedAsync(WebApplication app)
{
    using IServiceScope scope = app.Services.CreateScope();
    SsoAdminDbContext db = scope.ServiceProvider.GetRequiredService<SsoAdminDbContext>();
    await db.Database.MigrateAsync();

    if (!await db.LoginUsuarios.AnyAsync())
    {
        db.LoginUsuarios.Add(new LoginUsuario
        {
            Username = "admin",
            PasswordHash = BCrypt.Net.BCrypt.HashPassword("admin")
        });
        await db.SaveChangesAsync();
    }
}
