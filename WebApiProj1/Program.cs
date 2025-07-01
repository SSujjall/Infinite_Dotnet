using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using WebApiProj1.ActingDB;
using WebApiProj1.Data;
using WebApiProj1.Models.Config;
using WebApiProj1.Models.Entities;
using WebApiProj1.Repositories;
using WebApiProj1.Repositories.Common;
using WebApiProj1.Repositories.Interfaces;
using WebApiProj1.Services;
using WebApiProj1.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

#region Swagger Config for JWT
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebApiProj1", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme.",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme 
            {
                Reference = new OpenApiReference 
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});
#endregion

#region Config
var jwtSection = builder.Configuration.GetSection("JwtConfig");
builder.Services.Configure<JwtConfig>(jwtSection);
#endregion

#region Register DBContext with Inmemory provider
builder.Services.AddDbContext<AppDbContext>(opts =>
{
    opts.UseInMemoryDatabase("InMemoryDb");
});
#endregion

#region Register Identity Service
builder.Services.AddIdentity<IdtyUser, Roles>(opts =>
{
    opts.SignIn.RequireConfirmedAccount = false;
    opts.Password.RequireDigit = false;
    opts.Password.RequiredLength = 6;
    opts.Password.RequireNonAlphanumeric = false;
    opts.Password.RequireUppercase = false;
    opts.Password.RequireLowercase = false;
})
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();
#endregion

#region Register jwt
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
            ValidIssuer = builder.Configuration["JwtConfig:ValidIssuer"],
            ValidAudience = builder.Configuration["JwtConfig:ValidAudience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtConfig:SecretKey"]))
        };
    });
#endregion

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddAuthorization();

#region Repo/Services DI
builder.Services.AddSingleton<DumDb>();
builder.Services.AddSingleton<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddScoped<IAuthRepository, AuthRepository>();
builder.Services.AddScoped<IAuthService, AuthService>();

builder.Services.AddScoped<IBookRepository, BookRepository>();
builder.Services.AddScoped<IBookService, BookService>();
#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebApiProj1 v1"));
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

#region Seed default values
using (var scope = app.Services.CreateScope())
{
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdtyUser>>();
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<Roles>>();

    // Create Roles
    string[] roles = new[] {
        "Admin",
        "User"
    };

    foreach (var role in roles)
    {
        if (!await roleManager.RoleExistsAsync(role))
            await roleManager.CreateAsync(new Roles() { Id = Guid.NewGuid().ToString(), Name = "Admin" });
    }

    // Create a default admin user
    var adminUser = await userManager.FindByNameAsync("admin");
    if (adminUser == null)
    {
        adminUser = new IdtyUser
        {
            UserName = "admin",
            Email = "admin@example.com",
            EmailConfirmed = true,
            FullName = "admin"
        };

        var result = await userManager.CreateAsync(adminUser, "Admin123!"); // Use strong password
        if (result.Succeeded)
        {
            await userManager.AddToRoleAsync(adminUser, "Admin");
        }
    }
}
#endregion

app.Run();

