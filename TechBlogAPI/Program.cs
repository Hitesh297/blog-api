using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using TechBlog.Application;
using TechBlog.Infrastructure;
using TechBlog.Infrastructure.Data;
using TechBlogAPI.Middleware;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddApplication();

string connectionString;
if (Environment.GetEnvironmentVariable("JAWSDB_URL") != null)
{
    // Parse JAWSDB_URL
    var jawsDbUrl = Environment.GetEnvironmentVariable("JAWSDB_URL");
    connectionString = jawsDbUrl
        .Replace("mysql://", "Server=")
        .Replace(":", ";Port=")
        .Replace("@", ";User Id=")
        .Replace("/", ";Database=")
        .Replace("?", ";");
}
else
{
    // Fallback for local development
    connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
}
builder.Services.AddInfrastructure(connectionString);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp", builder =>
    {
        builder.WithOrigins("http://localhost:5173") // Adjust for your React app's URL
               .AllowAnyHeader()
               .AllowAnyMethod();
    });
});

// Add Identity
builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
{
    // Optional: Configure Identity options
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = true;
    options.Password.RequiredLength = 8;
})
.AddEntityFrameworkStores<TechBlogDbContext>()
.AddDefaultTokenProviders();


builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = "BlogAPI",
            ValidAudience = "hiteshpatel.dev",
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("JWT_SECRET") ?? "qwertyuioplkjhgfdsa125478963bvcf"))
        };
    });
builder.Services.AddAuthorization();

string port = Environment.GetEnvironmentVariable("PORT") ?? "5000";
builder.WebHost.UseUrls($"http://*:{port}");

var app = builder.Build();

app.UseExceptionHandlerMiddleware();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<TechBlogDbContext>();
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    await IdentitySeeder.SeedAsync(userManager, roleManager);
    await DatabaseSeeder.Seed(context);
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}




// In the middleware pipeline, before app.UseAuthorization():
app.UseCors("AllowReactApp");

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
