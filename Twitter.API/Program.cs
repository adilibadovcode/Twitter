using Microsoft.EntityFrameworkCore;
using Twitter.DAL.Contexts;
using Twitter.Business;
using Microsoft.AspNetCore.Identity;
using Twitter.Core.Entities;
using Twitter.Business.ExternalServices.Implements;
using Twitter.Business.ExternalServices.Interfaces;
using Twitter.Business.Services.Implements;
using Twitter.Business.Services.Interface;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<TwitterContext>(opt => opt.UseSqlServer
(builder.Configuration.GetConnectionString("MSSql"))).AddIdentity<AppUser, IdentityRole>(opt =>
{
    opt.SignIn.RequireConfirmedEmail = true;
    opt.User.RequireUniqueEmail = true;
    opt.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789._";
    opt.Lockout.MaxFailedAccessAttempts = 10;
    opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(1);
    opt.Password.RequireNonAlphanumeric = false;
    opt.Password.RequiredLength = 4;
}).AddDefaultTokenProviders().AddEntityFrameworkStores<TwitterContext>(); 

builder.Services.AddRepositories();
builder.Services.AddServices();
builder.Services.AddBusinessLayer();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
