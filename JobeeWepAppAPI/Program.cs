using BusinessObject.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Net.payOS;
using Repository;
using System.Text;
using Services;
using Services.Inteface;
using Repository.Interface;
using Microsoft.AspNetCore.Identity;
using Validaiton.User;

IConfiguration configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
#region GenerativeAI
var generativeApiKey = configuration["GenerativeAI:ApiKey"] ?? throw new Exception("Cannot find Generative AI API Key");
#endregion
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
#region JWT
var jwtIssuer = builder.Configuration.GetSection("JWT:Issuer").Get<string>();
var jwtAudience = builder.Configuration.GetSection("JWT:Audience").Get<string>();
var jwtKey = builder.Configuration.GetSection("JWT:Key").Get<string>();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
 .AddJwtBearer(options =>
 {
     options.TokenValidationParameters = new TokenValidationParameters
     {
         ValidateIssuer = true,
         ValidateAudience = true,
         ValidateLifetime = true,
         ValidateIssuerSigningKey = true,
         ValidIssuer = jwtIssuer,
         ValidAudience = jwtIssuer,
         IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
     };
 });
builder.Services.Configure<JwtBearerOptions>(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtIssuer,
        ValidAudience = jwtAudience,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey)),
    };
});
#endregion
#region Cloudinary
builder.Services.Configure<CloudinarySetting>(builder.Configuration.GetSection("CloudinarySetting"));
#endregion
#region PayOs
builder.Services.AddSingleton<PayOS>(provider =>
{
    var configuration = provider.GetRequiredService<IConfiguration>();

    return new PayOS
    (
        configuration["Environment:PAYOS_CLIENT_ID"] ?? throw new Exception("Cannot find environment"),
        configuration["Environment:PAYOS_API_KEY"] ?? throw new Exception("Cannot find environment"),
        configuration["Environment:PAYOS_CHECKSUM_KEY"] ?? throw new Exception("Cannot find environment")
    );
});
#endregion
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Name = "Authorization",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Description = "Please enter your token with this format: ''Bearer YOUR_TOKEN''",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
        BearerFormat = "JWT",
        Scheme = "bearer"
    });
    options.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Name = "Bearer",
                In = ParameterLocation.Header,
                Reference = new OpenApiReference
                {
                    Id = "Bearer",
                    Type = ReferenceType.SecurityScheme
                }
            },
            new List<string>()
        }
    });
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "JobeeAPI", Version = "v1" });
});

builder.Services.AddSingleton(generativeApiKey);
builder.Services.AddSingleton<IConfiguration>(builder.Configuration);
builder.Services.AddHttpContextAccessor();
builder.Services.AddDbContext<DBContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnectionString")));
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        policy =>
        {
            policy.WithOrigins("http://localhost:5173", "https://solva-app.vercel.app")
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});
#region AddScope
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddScoped<IApplicationService, ApplicationService>();
builder.Services.AddScoped<IApplicationRepository, ApplicationRepository>();
builder.Services.AddScoped<IJobService, JobService>();
builder.Services.AddScoped<IJobRepository, JobRepository>();
builder.Services.AddScoped<ISubcriptionPlanService, SubcriptionPlanService>();
builder.Services.AddScoped<ISubscriptionPlanRepository, SubcriptionPlanRepository>();
builder.Services.AddScoped<IPaymentRepository, PaymentRepository>();
builder.Services.AddScoped<IPaymentService, PaymentService>();
builder.Services.AddScoped<IChatGpt, ChatGptService>();
builder.Services.AddScoped<AccountValidation>();
#endregion
var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
    c.RoutePrefix = "swagger"; // Đặt đường dẫn cho Swagger UI
});
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAuthorization();
app.UseAuthentication();
app.UseCors();
app.MapControllers();
app.Run();
