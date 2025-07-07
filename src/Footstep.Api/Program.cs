using Footstep.Api.Filters;
using Footstep.Api.Middleware;
using Footstep.Application;
using Footstep.Infrastructure;
//V1.0.9
var builder = WebApplication.CreateBuilder(args);

builder.Configuration["ConnectionStrings:DefaultConnection"] = Environment.GetEnvironmentVariable("DEFAULT_CONNECTION");

var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";
builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(int.Parse(port));
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMvc(options => options.Filters.Add(typeof(ExceptionFilter)));

builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseMiddleware<CultureMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
