using System.Text;
using API.Data;
using API.Interfaces;
using API.MiddleWare;
using API.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddDbContext<AppDbContext>(opt =>
{
    opt.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
});


//######################################
//Allowing acces from a browser to the API

//First part for CORSE
//######################################
builder.Services.AddCors();
//#####################################

//####################################
//Register the token service with token service interface Three different lifetimes
/*
1 --> Singleton
2 --> Transient
3 --> Scoped
*/
builder.Services.AddScoped<ITokenService, TokenService>();
//####################################

//#############################################
/*
 --> 1 --> Add authentication as as service
 --> 2 --> Add middleware

/
*/
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    var tokenKey = builder.Configuration["TokenKey"] ?? throw new Exception("Token key not found - Program.cs");

    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenKey)),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});
//#############################################

var app = builder.Build();

// Configure the HTTP request pipeline.
//####################################
//Add custom middleware for exception handling
// Exception middleware is at the top
//Exception middleware
app.UseMiddleware<ExceptionMiddleWare>();

//#######################################
//Add header for CORSE as second part
app.UseCors(x =>
 x.AllowAnyHeader().
 AllowAnyMethod().
 WithOrigins("http://localhost:4200", "https://localhost:4200")
 );

//#######################################
//Add authentication middleware
//#######################################
app.UseAuthentication(); //Who are you?
app.UseAuthorization(); //Are you allowed?
//##############################


app.MapControllers();

app.Run();
