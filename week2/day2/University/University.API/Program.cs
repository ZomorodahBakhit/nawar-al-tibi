using Autofac;

using Autofac.Extensions.DependencyInjection;

using AutoWrapper;

using Microsoft.AspNetCore.Authentication.JwtBearer;

using Microsoft.AspNetCore.Identity;

using Microsoft.EntityFrameworkCore;

using Microsoft.IdentityModel.Tokens;

using Microsoft.OpenApi.Models;

using Serilog;

using System.Text;

using University.API.Configurations;

using University.API.Helpers;

using University.Core.Modules;

using University.Data;

using University.Data.Entities;

using University.Data.Modules;



var builder = WebApplication.CreateBuilder(args);



builder.Services.AddDbContext<UniversityDbContext>(options =>

    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));



builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

builder.Host.ConfigureContainer<ContainerBuilder>(container =>

{

    container.RegisterModule<RepositoryModule>();

    container.RegisterModule<ServiceModule>();

});



builder.Services.AddIdentity<User, Role>()

    .AddEntityFrameworkStores<UniversityDbContext>()

    .AddDefaultTokenProviders();



var jwtSettings = builder.Configuration.GetSection("JwtSettings");

builder.Services.Configure<JwtSettings>(jwtSettings);

builder.Services.AddAuthentication(options =>

{

    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;

    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

})

.AddJwtBearer(options =>

{

    var secretKey = Encoding.UTF8.GetBytes(jwtSettings["SecretKey"]!);

    options.TokenValidationParameters = new TokenValidationParameters

    {

        ValidateIssuer = true,

        ValidateAudience = true,

        ValidateLifetime = true,

        ValidateIssuerSigningKey = true,

        ValidIssuer = jwtSettings["Issuer"],

        ValidAudience = jwtSettings["Audience"],

        IssuerSigningKey = new SymmetricSecurityKey(secretKey)

    };

});

builder.Services.AddScoped<IJwtTokenHelper, JwtTokenHelper>();



builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();



app.UseAuthentication();

app.UseAuthorization();



if (app.Environment.IsDevelopment())

{

    app.UseSwagger();

    app.UseSwaggerUI();

}



app.UseApiResponseAndExceptionWrapper();

app.UseHttpsRedirection();

app.MapControllers();

app.Run();

