using System.Reflection;
using CohortsBookStore.Context;
using CohortsBookStore.Middlewares;
using CohortsBookStore.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<BookStoreDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("BookStoreDb")));
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<ILoggerService, ConsoleLogger>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.UseMiddleware<CustomExceptionMiddleware>();
app.MapControllers();
app.Run();
