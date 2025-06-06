using BE.Context;
using BE.DAO;
using BE.Services.Impl;
using BE.Services.Interfaces;
using BE.Ultility;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient();
builder.Services.AddHttpContextAccessor();
builder.Services.AddDbContext<JtContext>();
builder.Services.AddScoped<SysUserDAO>();
builder.Services.AddScoped<ItemDAO>();
builder.Services.AddScoped<StorageDAO>();
builder.Services.AddScoped<ExportDAO>();
builder.Services.AddScoped<CategoryDAO>();
builder.Services.AddScoped<ProductDAO>();
builder.Services.AddScoped<ImportDAO>();
builder.Services.AddScoped<SupplierDAO>();
builder.Services.AddScoped<UserServices>();
builder.Services.AddScoped<CustomerDAO>();
builder.Services.AddScoped<StatisticDAO>();

builder.Services.AddScoped<IImport,ImportImpl>();
builder.Services.AddScoped<IStatistic, StatisticImpl>();
builder.Services.AddScoped<ICustomer, CustomerImpl>();
builder.Services.AddScoped<ISysUser, SysUserImpl>();
builder.Services.AddScoped<IItem, ItemImpl>();
builder.Services.AddScoped<ISupplier, SupplierImpl>();
builder.Services.AddScoped<IStorage, StorageImpl>();    
builder.Services.AddScoped<IExport, ExportImpl>();
builder.Services.AddScoped<ICategory, CategoryImpl>();
builder.Services.AddScoped<IProduct, ProductImpl>();

builder.Services.AddScoped<JWTServices>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", policy =>
    {
        policy.AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials()
              .SetIsOriginAllowed(_ => true);
    });
});
builder.Services.AddSwaggerGen(opt =>
{
    var jwtSecScheme = new OpenApiSecurityScheme
    {
        BearerFormat = "JWT",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = JwtBearerDefaults.AuthenticationScheme,
        Description = "Access Token",
        Reference = new OpenApiReference
        {
            Id = JwtBearerDefaults.AuthenticationScheme,
            Type = ReferenceType.SecurityScheme
        }
    };
    opt.AddSecurityDefinition("Bearer", jwtSecScheme);
    opt.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    { jwtSecScheme, Array.Empty<string>() }
                });
});


builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, opt =>
{
    opt.RequireHttpsMetadata = false;
    opt.SaveToken = true;
    opt.TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = builder.Configuration["JwtConfig:Issuer"],
        ValidAudience = builder.Configuration["JwtConfig:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration["JwtConfig:Key"]!)),
        ValidateAudience = true,
        ValidateIssuer = true,
        ValidateIssuerSigningKey = true,
        ClockSkew = TimeSpan.Zero
    };
  

    opt.Events = new JwtBearerEvents
    {
        OnMessageReceived = context =>
        {
            var accessToken = context.Request.Query["access_token"];
            if (!string.IsNullOrEmpty(accessToken) &&
                context.HttpContext.WebSockets.IsWebSocketRequest)
            {
                context.Token = accessToken;
            }
            return Task.CompletedTask;
        }
    };
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("CorsPolicy");
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
