using EasyCookAPI;
using EasyCookAPI.Core;
using EasyCookAPI.Core.Helpers;
using EasyCookAPI.Core.Interfaces;
using EasyCookAPI.Core.Services;
using EasyCookAPI.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<EasyCookContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("EasyCookAPI")));

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IRecipeService, RecipeService>();
builder.Services.AddScoped<IIngredientService, IngredientsService>();
builder.Services.AddScoped<IStepService, StepService>();
builder.Services.AddScoped<ICommentService, CommentService>();
builder.Services.AddScoped<IFavService, FavService>();
builder.Services.AddScoped<IMapper, Mapper>();
builder.Services.AddScoped<IHelper, Helper>();

builder.Services.AddHttpContextAccessor();


builder.Services.AddControllers(options =>
{
    var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
    options.Filters.Add(new AuthorizeFilter(policy));
});


//AUTH WITH JWT
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Secret_key"] ?? string.Empty))
    };
});

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
