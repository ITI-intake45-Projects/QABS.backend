using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using QABS.Infrastructure;
using QABS.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Add DbContext
builder.Services.AddDbContext<QABSDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("QABScontext")));


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddScoped<EnrollmentRepository>();
builder.Services.AddScoped<StudentRepository>();
builder.Services.AddScoped<SessionRepository>();
builder.Services.AddScoped<StudentPaymentRepository>();
builder.Services.AddScoped<SubscribtionPlanRepository>();
builder.Services.AddScoped<TeacherRepository>();
builder.Services.AddScoped<TeacherPayoutRepositroy>();
builder.Services.AddScoped<TeacherAvailabilityRepository>();
builder.Services.AddScoped<UserRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
