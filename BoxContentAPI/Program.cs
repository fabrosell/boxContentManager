using BoxContentBusinessLogic.Interfaces;
using BoxContentBusinessLogic.Services;
using BoxContentCommon.Interfaces;
using BoxContentCommon.Interfaces.Fakes;
using BoxContentCommon.Mappings;
using BoxContentCommon.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSingleton<IBoxContentConfiguration, FakeBoxContentConfiguration>();
builder.Services.AddSingleton<IUserStorageService, UserStorageService>();
builder.Services.AddSingleton<IUserService, UserService>();
builder.Services.AddSingleton<IBoxStorageService, BoxStorageService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// MongoDB mappings
UserMap.Map();
BoxMap.Map();

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

//app.MapControllerRoute(
//    name: "default",
//    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
