using Hangfire;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using QABS.Infrastructure;
using QABS.Models;
using QABS.Repository;
using QABS.Service;
using System.Text;
using Utilities;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Add DbContext
//builder.Services.AddDbContext<QABSDbContext>(options =>
//    options.UseSqlServer(builder.Configuration.GetConnectionString("QABScontext")));


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//To Enable Swagger to test authentication token >>(Bearer space token)
builder.Services.AddSwaggerGen(c => {
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "JWTToken_Auth_API",
        Version = "v1"
    });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 1safsfsdfdfd\"",
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement {
        {
            new OpenApiSecurityScheme {
                Reference = new OpenApiReference {
                    Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});


builder.Services.AddScoped<UserRepository>();
builder.Services.AddScoped<DashboardService>();
builder.Services.AddScoped<AdminRepository>();
builder.Services.AddScoped<EnrollmentRepository>();
builder.Services.AddScoped<StudentRepository>();
builder.Services.AddScoped<SessionRepository>();
builder.Services.AddScoped<StudentPaymentRepository>();
builder.Services.AddScoped<SubscribtionPlanRepository>();
builder.Services.AddScoped<TeacherRepository>();
builder.Services.AddScoped<TeacherPayoutRepositroy>();
builder.Services.AddScoped<TeacherAvailabilityRepository>();
builder.Services.AddScoped<UserRepository>();
builder.Services.AddScoped<UnitOfWork>();
builder.Services.AddScoped<AccountService>();
builder.Services.AddScoped<EnrollmentService>();
builder.Services.AddScoped<StudentService>();
builder.Services.AddScoped<TeacherService>();
builder.Services.AddScoped<SubscribtionPlanService>();
builder.Services.AddScoped<SessionService>();

//builder.Services.AddScoped<UploadMedia>();


builder.Services.AddDbContext<QABSDbContext>
    (i => i.UseLazyLoadingProxies().UseSqlServer(builder.Configuration.GetConnectionString("QABScontext")));
builder.Services.AddIdentity<AppUser, IdentityRole>()
    .AddEntityFrameworkStores<QABSDbContext>();

builder.Services.AddHttpClient();

builder.Services.AddAuthentication(option =>
{
    option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    option.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
    option.DefaultSignOutScheme = JwtBearerDefaults.AuthenticationScheme;
    option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(option =>
{
    option.SaveToken = true;
    option.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateAudience = false,
        ValidateIssuer = false,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration["JWT:PrivateKey"]))
    };
    option.Events = new JwtBearerEvents
    {
        OnMessageReceived = context =>
        {
            var accessToken = context.Request.Query["access_token"];


            var path = context.HttpContext.Request.Path;
            if (!string.IsNullOrEmpty(accessToken) &&
            path.StartsWithSegments("/notificationhub"))
            {
                context.Token = accessToken;
            }
            return Task.CompletedTask;
        }
    };
});

builder.Services.AddHangfire(config =>
{
    config.UseSimpleAssemblyNameTypeSerializer()
          .UseRecommendedSerializerSettings()
          .UseSqlServerStorage(builder.Configuration.GetConnectionString("QABScontext"));
});
builder.Services.AddHangfireServer();


builder.Services.AddCors(options => {
    options.AddPolicy("AllowAngular", policy => {
        policy.WithOrigins("http://localhost:4200")
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });
});



var app = builder.Build();


app.UseCors("AllowAngular");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHangfireDashboard("/hangfire");
RecurringJob.AddOrUpdate<SessionService>(
    "HangFireUpdateSessions",
    s => s.HangFireUpdateSessions(),
    Cron.Daily);

RecurringJob.AddOrUpdate<EnrollmentService>(
    "HangFireUpdateEnrollments",
    s => s.HangFireUpdateEnrollments(),
    Cron.Daily);

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
