using Dapper;
using SsoAdmin.Application.Interfaces;
using SsoAdmin.Application.UseCases;
using SsoAdmin.Infrastructure.Data;
using SsoAdmin.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

SqlMapper.AddTypeHandler(new DateOnlyTypeHandler());

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

string connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? "Data Source=ssoadmin.db";

builder.Services.AddSingleton(new DbConnectionFactory(connectionString));
builder.Services.AddSingleton<SchemaInitializer>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ICredentialRepository, CredentialRepository>();
builder.Services.AddScoped<IApplicationRepository, ApplicationRepository>();
builder.Services.AddScoped<IPermissionRepository, PermissionRepository>();
builder.Services.AddScoped<IVerificarAccesoUseCase, VerificarAccesoUseCase>();

var app = builder.Build();

app.Services.GetRequiredService<SchemaInitializer>().Initialize();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
