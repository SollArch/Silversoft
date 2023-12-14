using Autofac;
using Autofac.Extensions.DependencyInjection;
using AutoMapper;
using Business.Concrete;
using Business.DependencyResolvers.Autofac;
using Business.MappingProfiles.User;
using Business.Rules;
using Core.DependencyResolver;
using Core.Extensions;
using Core.Utilities.IoC;
using Core.Utilities.Security.Encryption;
using Core.Utilities.Security.Jwt;
using DataAccess.Concrete.EntityFramework;
using DataAccess.Context;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddTransient<DatabaseContext>();

var claimManager = new OperationClaimManager(new EfOperationClaimDal(), new OperationClaimRules(new EfOperationClaimDal()));
claimManager.AddAdminClaim();
var userManager = new UserManager(new EfUserDal(),new UserRules(new EfUserDal()));
userManager.AddAdmin();
var userOperationClaimManager = new UserOperationClaimManager(new EfUserOperationClaimDal());
userOperationClaimManager.AddAdminClaimToAdminUser();

builder.Services.AddHttpContextAccessor();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors();
builder.Services.AddAutofac();
builder.Services.AddDependencyResolvers(new ICoreModule[]
{
    new CoreModule()
});

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory()).ConfigureContainer<ContainerBuilder>(
    containerBuilder =>
    {
        containerBuilder.RegisterModule(new AutofacBusinessModule());
    } );
var mappingConfig = new MapperConfiguration(mc =>
{
    mc.AddProfile(new UserMappingProfile());
});
var tokenOptions = builder.Configuration.GetSection("TokenOptions").Get<TokenOptions>();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidIssuer = tokenOptions!.Issuer,
        ValidAudience = tokenOptions.Audience,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = SecurityKeyHelper.CreateSecurityKey(tokenOptions.SecurityKey)
    };
});
builder.Services.AddSwaggerGen(opt =>
{
    opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description =
            "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345.54321\""
    });
    opt.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
                { Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" } },
            Array.Empty<string>()
        }
    });
});


IMapper mapper = mappingConfig.CreateMapper();
builder.Services.AddSingleton(mapper);
var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();
app.UseAuthentication();
app.UseCors(corsPolicyBuilder => corsPolicyBuilder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
app.ConfigureCustomExceptionMiddleware();
app.MapControllers();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});
app.Run();