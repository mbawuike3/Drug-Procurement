using Drug_Procurement.Context;
using Drug_Procurement.Security.Hash;
using Drug_Procurement.SeederDb;
using Drug_Procurement.Validations;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddValidatorsFromAssembly(typeof(RoleValidator).Assembly);
builder.Services.AddValidatorsFromAssembly(typeof(OrderValidator).Assembly);
builder.Services.AddValidatorsFromAssembly(typeof(UserValidator).Assembly);
builder.Services.AddValidatorsFromAssembly(typeof(InventoryValidator).Assembly);
builder.Services.AddMediatR(x => x.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("AccountingConnection"));
});
builder.Services.Initialize(builder.Services.BuildServiceProvider()).GetAwaiter().GetResult();
builder.Services.AddScoped<IPasswordService, PasswordService>();
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
