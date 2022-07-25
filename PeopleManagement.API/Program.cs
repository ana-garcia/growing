using FluentValidation;
using Microsoft.EntityFrameworkCore;
using PeopleManagement.API;
using PeopleManagement.API.DBContext;
using PeopleManagement.API.Model;
using PeopleManagement.API.Services;
using PeopleManagement.API.Validators;
using Serilog;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .WriteTo.Console()
    .WriteTo.File("logs/peopleinfo.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

var builder = WebApplication.CreateBuilder(args);
//builder.Logging.ClearProviders();
//builder.Logging.AddConsole();
builder.Host.UseSerilog();

// Add services to the container.
builder.Services.AddControllers( options =>
{
    options.ReturnHttpNotAcceptable = true;
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IValidator<PersonModel>, PersonValidator>();
builder.Services.AddScoped<IValidator<RegistrationForCreationModel>, RegistrationValidator>();
builder.Services.AddSingleton<PeopleDataStore>();

// for production is missing the enviroment variable
builder.Services.AddDbContext<PersonInfoContext>(
    dbContextOptions => dbContextOptions.UseSqlite(
        builder.Configuration["ConnectionStrings:PersonInfoDBConnectionString"]));

builder.Services.AddScoped<IPersonInfoRepository, PersonInfoRepository>();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.MapControllers();

app.Run();
