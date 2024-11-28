using DAL;
using API.Endpoints;
using Microsoft.EntityFrameworkCore;
using Messaging;
using StockMarket.Bootstrapper;
using API.HostedServices;
using Serilog;
using CartCloud;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

string defaultConnection = builder.Configuration.GetConnectionString("DefaultConnection")!;

builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(defaultConnection));

builder.Services.AddSwaggerGen();

builder.Services
    .ConfigureAuthorization(builder.Configuration)
    .CongigureReposAndServices()
    .AddPolygonProviderServices(builder.Configuration)
    .AddMessagingServices(builder.Configuration);

builder.Services.AddHostedService<PolygonNewsUpdateService>();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
        {
            builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
        });
});

builder.Host.UseSerilog((context, configuration) => configuration.ReadFrom.Configuration(context.Configuration));

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

using (var serviceScope = app.Services.GetService<IServiceScopeFactory>().CreateScope())
{
    var context = serviceScope.ServiceProvider.GetRequiredService<AppDbContext>();
    context.Database.EnsureCreated();
}

app.UseSerilogRequestLogging();

app.UseHttpsRedirection();
app.UseCors();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.MapGroup("api/Users").MapUserEndpoints();
app.MapGroup("api/Polygon").MapPolygonEndPoints();
app.MapGroup("api/Carts").MapCartEndpoints();

app.Run();
