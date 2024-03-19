using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using UrbanLoom_B.DBcontext;
using UrbanLoom_B.JWT;
using UrbanLoom_B.Mapper;
using UrbanLoom_B.Services.CartService;
using UrbanLoom_B.Services.CategoryService;
using UrbanLoom_B.Services.OrderService;
using UrbanLoom_B.Services.ProductService;
using UrbanLoom_B.Services.UserService;
using UrbanLoom_B.Services.WhishListService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<DbContextClass>();
builder.Services.AddAutoMapper(typeof(Mapper_UL));
builder.Services.AddScoped<ICart , CartService>();
builder.Services.AddScoped<ICategory , CategoryService>();
builder.Services.AddScoped<IOrder , OrderService>();
builder.Services.AddScoped<IProduct ,  ProductService>();
builder.Services.AddScoped<IUserService , UserService>();
builder.Services.AddScoped<IWhishList , WhishListService>();
builder.Services.AddScoped<IJwt, Jwt>();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(o =>
{
    o.TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey
        (Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true
    };
});

builder.Services.AddCors(op =>
{
    op.AddPolicy("ReactPolicy", builder =>
    {
        builder.AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("ReactPolicy");
app.UseStaticFiles();               
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
