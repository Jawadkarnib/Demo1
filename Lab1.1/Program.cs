
using Lab1._1.Filters;
using Lab1._1.Middlewares;
using Lab1._1.Services;
using Microsoft.OpenApi.Models;
using Serilog;
using Swashbuckle.AspNetCore.SwaggerGen;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File("Logs/applog-.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

builder.Host.UseSerilog();  // Use Serilog for hosting

builder.Services.AddLogging(loggingBuilder =>
{
    // Add Serilog to the logging services
    loggingBuilder.AddSerilog();
});
// Add services to the container.
builder.Services.AddScoped<IStudentService, StudentService>();
builder.Services.AddScoped<CallerHeaderFilter>();

builder.Services.AddControllers(options =>
{
    options.Filters.Add<CallerHeaderFilter>();
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Your API", Version = "v1" });

    c.OperationFilter<AddHeadersOperationFilter>();
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
  
}

app.UseLoggingMiddleware();
app.UseSerilogRequestLogging();  



app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

