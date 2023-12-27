using Microsoft.EntityFrameworkCore;
using OrganizacnaStrukturaFirmy.Controllers.Filters.ActionFilters;
using OrganizacnaStrukturaFirmy.Controllers.Filters.ExceptionFilters;
using OrganizacnaStrukturaFirmy.Data;
using OrganizacnaStrukturaFirmy.Models.Validations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddScoped<Employee_ValidateEmployeeIdAttribute>();
builder.Services.AddScoped<Node_ValidateNodeIdAttribute>();
builder.Services.AddScoped<Node_HandleUpdateExceptionsFilterAttribute>();
builder.Services.AddScoped<Employee_HandleUpdateExceptionsFilterAttribute>();
builder.Services.AddScoped<Node_LevelAttribute>();
builder.Services.AddScoped<Employee_ValidateWorkplaceIdExistanceFilterAttribute>();
builder.Services.AddScoped<Node_ValidateHeadEmployeeExistanceFilterAttribute>();
builder.Services.AddScoped<Node_ValidateParentNodeExistanceFilterAttribute>();
builder.Services.AddScoped<Node_ValidateDeleteFilterAttribute>();
builder.Services.AddScoped<Employee_ValidateDeleteFilterAttribute>();
builder.Services.AddScoped<Employee_ValidateId_WorkplaceFilterAttribute>();
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
