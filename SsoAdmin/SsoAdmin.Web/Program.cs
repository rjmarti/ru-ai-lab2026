using Dapper;
using SsoAdmin.Application.Interfaces;
using SsoAdmin.Application.Services;
using SsoAdmin.Application.UseCases;
using SsoAdmin.Infrastructure.Data;
using SsoAdmin.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

SqlMapper.AddTypeHandler(new DateOnlyTypeHandler());

builder.Services.AddRazorPages();

string connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? "Data Source=ssoadmin.db";

builder.Services.AddSingleton(new DbConnectionFactory(connectionString));
builder.Services.AddSingleton<SchemaInitializer>();

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ICredentialRepository, CredentialRepository>();
builder.Services.AddScoped<IApplicationRepository, ApplicationRepository>();
builder.Services.AddScoped<IPermissionRepository, PermissionRepository>();

builder.Services.AddScoped<IAdminQueryService, AdminQueryService>();
builder.Services.AddScoped<ICreateUsuarioUseCase, CreateUsuarioUseCase>();
builder.Services.AddScoped<IEditUsuarioUseCase, EditUsuarioUseCase>();
builder.Services.AddScoped<IDeactivateUsuarioUseCase, DeactivateUsuarioUseCase>();
builder.Services.AddScoped<ICreateCredencialUseCase, CreateCredencialUseCase>();
builder.Services.AddScoped<IDeleteCredencialUseCase, DeleteCredencialUseCase>();
builder.Services.AddScoped<ICreateAplicacionUseCase, CreateAplicacionUseCase>();
builder.Services.AddScoped<IEditAplicacionUseCase, EditAplicacionUseCase>();
builder.Services.AddScoped<IDeleteAplicacionUseCase, DeleteAplicacionUseCase>();

var app = builder.Build();

app.Services.GetRequiredService<SchemaInitializer>().Initialize();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.MapRazorPages();
app.Run();
