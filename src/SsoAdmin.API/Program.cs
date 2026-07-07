using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi;
using SsoAdmin.Application.Interfaces;
using SsoAdmin.Application.Services;
using SsoAdmin.Data;
using SsoAdmin.Models;
using System.Reflection;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "SsoAdmin API",
        Version = "v1",
        Description = "Backend para la administración centralizada de accesos SSO."
    });

    string xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    string xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    options.IncludeXmlComments(xmlPath);
});

builder.Services.AddDbContext<SsoAdminDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("Default")));

builder.Services.AddScoped<ISsoService, SsoService>();
builder.Services.AddScoped<ILoginService, LoginService>();
builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddScoped<ICredencialService, CredencialService>();
builder.Services.AddScoped<IAplicacionService, AplicacionService>();
builder.Services.AddScoped<IPermisoService, PermisoService>();

WebApplication app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "SsoAdmin API v1");
        options.RoutePrefix = "swagger";
    });
}

await SeedAsync(app);

app.MapControllers();

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
