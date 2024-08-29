using EasyCookAPI;
using EasyCookAPI.Core;
using EasyCookAPI.Core.Helpers;
using EasyCookAPI.Core.Interfaces;
using EasyCookAPI.Core.Services;
using EasyCookAPI.Models;
using Microsoft.EntityFrameworkCore;

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
