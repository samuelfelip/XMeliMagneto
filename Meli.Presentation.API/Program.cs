using Meli.Application.Services;
using Meli.Data.EF;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Meli.Data.Domain;

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
var builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration;
IWebHostEnvironment environment = builder.Environment;

var allowedHostCors = configuration.GetSection("CORS:AllowedOrigins").Get<string[]>();
//builder.Services.AddCors(options =>
//{
//    options.AddPolicy(MyAllowSpecificOrigins,
//                          policy =>
//                          {
//                              policy.WithOrigins(allowedHostCors)
//                                                  .AllowAnyHeader()
//                                                  .AllowAnyMethod();
//                          });
//});

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//if (environment.IsProduction())
//{
Console.WriteLine("--> Using SqlServer DB");
builder.Services.AddDbContext<AppDbContext>(opt =>
                 opt.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
        x => x.MigrationsAssembly("Meli.Presentation.API")));
Console.WriteLine($"Server DB {configuration.GetConnectionString("DefaultConnection")}");
//}
//else
//{

//    Console.WriteLine("--> Using InMem DB");
//    builder.Services.AddDbContext<UnitOfWork>(opt =>
//                     opt.UseInMemoryDatabase("InMem"));
//}

#region Injection Services

builder.Services.AddScoped<IDNAService, DNAService>();
#endregion

#region Injection Services

builder.Services.AddScoped<IDNARepository, DNARepository>();

#endregion

// Adding Authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.SaveToken = true;
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidAudience = configuration["JWT:ValidAudience"],
        ValidIssuer = configuration["JWT:ValidIssuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]))
    };
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(MyAllowSpecificOrigins);
// Authentication & Authorization
app.UseAuthentication();
//app.UseAuthorization();

app.MapControllers();



app.Run();
