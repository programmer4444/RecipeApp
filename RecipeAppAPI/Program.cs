using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using RecipeAppAPI.Models;
using System;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Use Azure.Identity credentials to authenticate with Azure Key Vault
var credential = new DefaultAzureCredential();

// Create a SecretClient to access the Key Vault
var keyVaultUri = new Uri("https://recipeappvault.vault.azure.net/");
var client = new SecretClient(keyVaultUri, credential);

// Retrieve the connection string from Azure Key Vault
KeyVaultSecret secret = client.GetSecret("DefaultConnection");
string connectionString = secret.Value;

// Add DbContext to DI container
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
