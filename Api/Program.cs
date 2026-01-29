using System.Text;
using Api;
using Application.Services.Implementations;
using Application.Services.Interfaces;
using Domain.Repositories;
using Infrastructure;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(x => x.EnableAnnotations());

builder.Services.AddDbContext<FocusFlowContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("MsSQL")));

AddScopedServices(builder);

builder.Services.AddHttpContextAccessor();
builder.Services.AddAuthorization();

AddJwtAuthentication(builder);

var  corsPolicy = "angularPolicy";

builder.Services.AddCors(x => x.AddPolicy(corsPolicy, 
    y => y.WithOrigins("http://localhost:4200")
    .AllowAnyMethod()
    .AllowAnyHeader()
    .AllowCredentials()));

builder.Services.AddControllers();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(corsPolicy);
app.UseMiddleware<ApiExceptionMiddleware>();
app.UseAuthentication();
app.UseAuthorization();
app.UseHttpsRedirection();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var context = services.GetRequiredService<FocusFlowContext>();    
    context.Database.Migrate();
}

app.Run();

void AddJwtAuthentication(WebApplicationBuilder webApplicationBuilder)
{
    webApplicationBuilder.Services
        .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,

                ValidIssuer = webApplicationBuilder.Configuration.GetValue<string>("Jwt:Issuer"),
                ValidAudience = webApplicationBuilder.Configuration.GetValue<string>("Jwt:Audience"),
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(webApplicationBuilder.Configuration.GetValue<string>("Jwt:Key")!)),
                ClockSkew = TimeSpan.Zero,
            };
        });
}

void AddScopedServices(WebApplicationBuilder webApplicationBuilder)
{
    webApplicationBuilder.Services.AddScoped<IUserService, UserService>();
    webApplicationBuilder.Services.AddScoped<IProjectService, ProjectService>();
    webApplicationBuilder.Services.AddScoped<IFlowTaskService, FlowTaskService>();
    webApplicationBuilder.Services.AddScoped<IReferenceDataService, ReferenceDataService>();
    webApplicationBuilder.Services.AddScoped<IAuthService, AuthService>();
    
    webApplicationBuilder.Services.AddScoped<IUserRepository, UserRepository>();
    webApplicationBuilder.Services.AddScoped<IProjectRepository, ProjectRepository>();
    webApplicationBuilder.Services.AddScoped<IFlowTaskRepository, FlowTaskRepository>();
    webApplicationBuilder.Services.AddScoped<IPriorityRepository, PriorityRepository>();
    webApplicationBuilder.Services.AddScoped<IStatusRepository, StatusRepository>();
}