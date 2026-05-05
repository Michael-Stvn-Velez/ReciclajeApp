using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ReciclajeApp.Api.Errors;
using ReciclajeApp.Application.UseCases.Permissions;
using ReciclajeApp.Application.UseCases.Roles;
using ReciclajeApp.Domain.Interfaces.Permissions;
using ReciclajeApp.Domain.Interfaces.Roles;
using ReciclajeApp.Infrastructure.Persistence;
using ReciclajeApp.Infrastructure.Repositories.Permissions;
using ReciclajeApp.Infrastructure.Repositories.Roles;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("Default")
    ?? throw new InvalidOperationException("Connection string 'Default' not found.");

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(connectionString));
builder.Services.AddScoped<IPermissionRepository, PermissionRepository>();
builder.Services.AddScoped<IRoleRepository, RoleRepository>();
builder.Services.AddScoped<PermissionUseCases>();
builder.Services.AddScoped<RoleUseCases>();
builder.Services.AddControllers()
    .ConfigureApiBehaviorOptions(options =>
    {
        options.InvalidModelStateResponseFactory = context =>
        {
            var errors = context.ModelState.Values
                .SelectMany(value => value.Errors)
                .Select(error => string.IsNullOrWhiteSpace(error.ErrorMessage)
                    ? "Error de validacion."
                    : error.ErrorMessage)
                .ToArray();

            var response = new ApiErrorResponse
            {
                Type = "validation_error",
                Message = "La solicitud contiene errores de validacion.",
                Errors = errors,
                TraceId = context.HttpContext.TraceIdentifier
            };

            return new BadRequestObjectResult(response);
        };
    });

var app = builder.Build();

app.UseMiddleware<GlobalExceptionMiddleware>();
app.MapControllers();

app.Run();
