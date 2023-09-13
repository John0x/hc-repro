using System.Security.Claims;
using System.Text;
using DataAnnotatedModelValidations;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Npgsql;
using EntityFramework.Exceptions.PostgreSQL;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders();
builder.Logging.AddConsole();


builder.Services
    .AddGraphQLServer()
    .AddMutationConventions(
        new MutationConventionOptions
        {
            InputArgumentName = "input",
            InputTypeNamePattern = "{MutationName}Input",
            PayloadTypeNamePattern = "{MutationName}Payload",
            PayloadErrorTypeNamePattern = "{MutationName}Error",
            PayloadErrorsFieldName = "errors",
            ApplyToAllMutations = true,
        })
    .AddProjections()
    .AddDataAnnotationsValidator()
    .AddTypes()
    .AddSorting()
    .AddFiltering()
    .AllowIntrospection(true)
    .ModifyRequestOptions(opt => opt.IncludeExceptionDetails = true);

builder.Services.AddHealthChecks();

builder.Services.AddCors();

var app = builder.Build();
app.UseCors(x => x
                  .AllowAnyMethod()
                  .AllowAnyHeader()
                  .SetIsOriginAllowed(origin => true) // allow any origin
                  .AllowCredentials()); // allow credentials

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{

}

app.UseHttpsRedirection();


app.UseHealthChecks("/health");
app.MapGraphQL();

app.Run();