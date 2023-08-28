using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using ProductAPI.Model.Helper;
using ProductAPI.Repository;
using ProductAPI.Services;
using ProductAPI.Services.FileServices;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//Repository Dependency
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("deafultDB")));
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ICatagoryRepository, CatagoryRepository>();
builder.Services.AddScoped<IAuthRepository, AuthRepository>();
builder.Services.AddScoped<IFileService, FileService>();
builder.Services.AddScoped<IShoppingCartRepository, ShoppingCartRepository>();
builder.Services.AddCors(options => options.AddPolicy("default", policy => policy.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod()));
//auto-mapper Dependency
var automapper = new MapperConfiguration(item => item.AddProfile(new MapperHandler()));
IMapper mapper = automapper.CreateMapper();
builder.Services.AddSingleton(mapper);

builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("versecretkey123456789")),
        ValidateAudience = false,
        ValidateIssuer = false,
        ClockSkew = TimeSpan.Zero
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
app.UseCors("default");
app.UseAuthentication();
app.UseAuthorization();
app.UseStaticFiles(
    new StaticFileOptions
    {
        FileProvider = new PhysicalFileProvider(Path.Combine(builder.Environment.ContentRootPath, "uploads")),
        RequestPath = "/Resources"
    });
app.MapControllers();

app.Run();
