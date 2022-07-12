using Serilog;
using Serilog.Events;
using HotelListing.Data;
using Microsoft.EntityFrameworkCore;
using HotelListing.Mappers;
using HotelListing.Units;
using Microsoft.AspNetCore.Identity;

string rootPath = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..\\..\\..\\"));
var builder = WebApplication.CreateBuilder(args);

// Logging
Log.Logger = new LoggerConfiguration()
    .WriteTo.File(
        Path.Combine(rootPath, "Logging\\log-.txt"),
        outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}",
        rollingInterval: RollingInterval.Hour,
        restrictedToMinimumLevel: LogEventLevel.Information)
    .CreateLogger();
builder.Host.UseSerilog();

// CORS
builder.Services.AddCors(o =>
{
    o.AddPolicy(
        "AllowAll",
        builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()
    );
});

// Mappers
builder.Services.AddAutoMapper(typeof(MapperInitializer));

// Database
builder.Services.AddDbContext<DatabaseContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DbConnetion"));
});

// Identity
builder.Services.AddAuthentication();
builder.Services.AddIdentity<ApiUser, IdentityRole>(o =>
{
    o.User.RequireUniqueEmail = true;
    o.Password.RequireDigit = false;
    o.Password.RequireUppercase = false;
    o.Password.RequireNonAlphanumeric = false;
}).AddEntityFrameworkStores<DatabaseContext>().AddDefaultTokenProviders();

//
builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();

// Add services to the container.
builder.Services.AddControllers().AddNewtonsoftJson(o =>
{
    o.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseSwagger();
app.UseSwaggerUI();

app.UseRouting();

app.UseAuthorization();

app.UseEndpoints(builder =>
{
    builder.MapControllers();
});


app.UseCors("AllowAll");

app.UseHttpsRedirection();


try
{
    Log.Information("Application Is Starting");
    app.Run();
}
catch (Exception e)
{
    Log.Fatal(e, "Application Failed to start");
}
finally
{
    Log.CloseAndFlush();
}