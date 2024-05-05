using Microsoft.EntityFrameworkCore;
using System.Reflection;
using WebApplication1.Data;
using WebApplication1.Helpers;
using WebApplication1.Repository;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

// old connection string
// "DefaultConnection": "Server=(localdb)\\ProjectModels;Database=5D-Task;Trusted_Connection=True;MultipleActiveResultSets=true"

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IFileUpload, FileUpload>();

builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

//if (app.Environment.IsDevelopment())
//{
    app.UseSwagger();
    app.UseSwaggerUI();
//}

app.UseStatusCodePagesWithReExecute("/Error/{0}");

app.UseCors(c =>
{
    c.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod();
});

app.UseStaticFiles();

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.MapControllers();

var scope = app.Services.CreateScope();
using var AppDbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
var Logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
try
{
    await AppDbContext.Database.MigrateAsync();

}
catch (Exception ex)
{
    Logger.LogError(ex, ex.Message);

}

app.Run();


