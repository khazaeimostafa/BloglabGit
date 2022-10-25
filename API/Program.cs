using System.Text;
using API.Authorizing;
using API.Extensions;
using API.Filters;
using API.MiddleWares;
using API.Seed;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// ..........................................................

TokenValidationParameters tokenValidationParameters = new TokenValidationParameters
{
    ValidateIssuerSigningKey = true,
    
    IssuerSigningKey =
                    new SymmetricSecurityKey(Encoding
                            .ASCII
                            .GetBytes(builder.Configuration["JWT:secret"])),
    ValidIssuer = builder.Configuration["JWT:Issuer"],
    ValidateIssuer = true,
    ValidAudience = builder.Configuration["JWT:Audience"],

    ValidateLifetime = true,
    ClockSkew = TimeSpan.Zero,


};

// ..........................................................................


builder.Services.ApiErrorExtension();
builder.Services.DependencyExtend(builder.Configuration);

builder.Services.AddSwaggerGen();

builder.Services.AddAuthorize(builder.Configuration);

builder.Services.ConnectDatabaseExtend(builder.Configuration);

builder.Services.AddMet(builder.Configuration);

builder.Services.Configure<MvcOptions>(opts => opts.Filters.Add<HttpsOnlyAttribute>());

builder.Services.AddSingleton(tokenValidationParameters);

builder.Services.JwtExtend(builder.Configuration, tokenValidationParameters);
// Add services to the container.

builder.Services.AddControllers().AddNewtonsoftJson(options =>
{
    options.SerializerSettings.ReferenceLoopHandling =
     Newtonsoft.Json.ReferenceLoopHandling.Ignore;
    options.SerializerSettings.ContractResolver = new DefaultContractResolver();

}

);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

builder.Services.AddSwaggerDocumentation();

//  builder.Services.CorsExtend(builder.Configuration);

builder.Services.AddCors(opt =>
{
    opt.AddPolicy("CorsPolicy", policy =>
    {
        policy.AllowAnyHeader().AllowAnyMethod().WithOrigins();
    });
});


builder.Services.Configure<DataProtectionTokenProviderOptions>(x =>
{
    x.TokenLifespan = TimeSpan.FromDays(1);
});

builder.Services.ConfigureApplicationCookie(x =>
x.AccessDeniedPath = new PathString("/api/AccountController/AccessDenied"));

builder.Services.AddMvc(options=>{
    
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    // app.UseSwagger();
    // app.UseSwaggerUI();

    app.UseSwaggerDocumentation();
    app.UseHttpsRedirection();

}
app.UseMiddleware<ExceptionMiddleWare>();
app.UseStatusCodePagesWithReExecute("/errors/{0}");

app.ConfigureExceptionHandler();

app.UseStaticFiles();


app.UseCors(x => x.AllowAnyHeader()
.AllowAnyMethod().AllowAnyOrigin()
);

app.UseAuthentication();



app.UseAuthorization();

app.MapControllers();
app.SeedRolesToDb().Wait();
app.Run();
