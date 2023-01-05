// using Microsoft.AspNetCore.Mvc.NewtonsoftJson;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MIFin.Api.BL;
using MIFin.Api.Data;

using MIFinApi.Authentication;
using Newtonsoft.Json.Serialization;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCors();

builder.Services
         .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
         //.AddJwtBearer(options => {
         //    options.TokenValidationParameters = new TokenValidationParameters() {
         //        ValidateIssuer = true,
         //        ValidateAudience = true,
         //        ValidateLifetime = true,
         //        ValidateIssuerSigningKey = true,
         //        ValidAudience = Configuration["Jwt:Audience"],
         //        ValidIssuer = Configuration["Jwt:Issuer"],
         //        IssuerSigningKey = new SymmetricSecurityKey(
         //            Encoding.UTF8.GetBytes(Configuration["Jwt:Key"])
         //        )
         //    };
         //})
        .AddScheme<AuthenticationSchemeOptions, ApiKeyAuthenticationHandler>("ApiKey", options => { });


//builder.Services.AddControllers();
builder.Services.AddControllers().AddNewtonsoftJson(options => {
    options.SerializerSettings.ContractResolver = new DefaultContractResolver();//disable automatic camelCase
});

builder.Services.AddScoped<DataRepository>();
builder.Services.AddScoped<ConnectopSvc>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => {
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Api Key Auth", Version = "v1" });
    c.AddSecurityDefinition("ApiKey", new OpenApiSecurityScheme {
        Description = "ApiKey must appear in header 24c123ce-7b7a-4f5f-916f-a1ec55537ceb",
        Type = SecuritySchemeType.ApiKey,
        Name = "ApiKey", //"XApiKey",
        In = ParameterLocation.Header,
        Scheme = "ApiKeyScheme"
      
    });
    var key = new OpenApiSecurityScheme() {
        Reference = new OpenApiReference {
            Type = ReferenceType.SecurityScheme,
            Id = "ApiKey"
        },
        In = ParameterLocation.Header
    };
    var requirement = new OpenApiSecurityRequirement{{ key, new List<string>() }};
    
    
    c.AddSecurityRequirement(requirement);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
//}
//app.UseMiddleware<ApiKeyMiddleware>();
app.UseHttpsRedirection();

app.UseCors(x => x
                .AllowAnyMethod()
                .AllowAnyHeader()
                .SetIsOriginAllowed(origin => true) // allow any origin
                .AllowCredentials()); // allow credentials

app.UseAuthorization();

app.MapControllers();

app.Run();
