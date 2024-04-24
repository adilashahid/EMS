using EMS.Business.Business;
using EMS.Business.Interfaces;
using EMS.DAL.Data;
using EMS.DAL.Interfaces;
using EMS.DAL.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
{
    Description = "JWT Authorization header using the bearer schema. Enter bearer {space} add your token in the text input. Example: Bearer swersdf8978sdf",
    Name = "Authorization",
    In = ParameterLocation.Header,
    Scheme = "Bearer"
});
    options.AddSecurityRequirement(new OpenApiSecurityRequirement()
{
    {
        new OpenApiSecurityScheme
        {
            Reference = new OpenApiReference
            {
                Id = "Bearer",
                Type = ReferenceType.SecurityScheme
            },
            Scheme = "oauth2",
            In = ParameterLocation.Header

        },
        new List<string>()
    }
});
});

var connectionString = builder.Configuration["ConnectionStrings:DefaulConnection"];
builder.Services.AddDbContext<EMSDbContext>(options =>
    options.UseSqlServer(connectionString));

#region Businesses servcies
builder.Services.AddTransient<IStudentBusiness, StudentBusiness>();
builder.Services.AddTransient<ITeacherBusiness, TeacherBusiness>();
builder.Services.AddTransient<ILoginBusiness, LoginBusiness>();
#endregion


#region Repositories services 
builder.Services.AddTransient<IStudentRepository, StudentRepository>();
builder.Services.AddTransient<ITeacherRepository, TeacherRepository>();
builder.Services.AddTransient<ILoginRepository, LoginRepository>();

#endregion



//var JWTsecretForLocal = Encoding.ASCII.GetBytes(builder.Configuration.GetValue<string>("JWTsecretForLocal"));
//string LocalAudience = builder.Configuration.GetValue<string>("LocalAudience");

//string LocalIssur = builder.Configuration.GetValue<string>("LocalIssur");

//builder.Services.AddAuthentication(options =>
//{
//    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
//    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

//}).AddJwtBearer("LoginForLocalUsers", options =>
//{
//    options.SaveToken = true;
//    options.TokenValidationParameters = new TokenValidationParameters()
//    {
//        ValidateIssuerSigningKey = true,
//        IssuerSigningKey = new SymmetricSecurityKey(JWTsecretForLocal),
//        ValidateIssuer = true,
//        ValidIssuer = LocalIssur,
//        ValidateAudience = true,
//        ValidAudience = LocalAudience
//    };
//});
var jwtSettingsSection = builder.Configuration.GetSection("JwtSettings");
var jwtSecret = jwtSettingsSection.GetValue<string>("Secret");
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        //ValidIssuer = jwtSettingsSection.GetValue<string>("Issuer"),
                        //ValidAudience = jwtSettingsSection.GetValue<string>("Audience"),
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSecret))
                    };
                });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.UseAuthorization();
app.UseAuthorization();


app.MapControllers();



app.Run();
