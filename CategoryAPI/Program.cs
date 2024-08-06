using CategoryAPI.Data;
using CategoryAPI.Service;
using DotNetCore.CAP;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// CAP Configuration
builder.Services.AddDbContext<ApiContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnectionString"));
});

builder.Services.AddCap(options =>
{
    options.DefaultGroupName = "Product";
    options.UseDashboard(); // CAP Dashboard'u etkinle?tirin
    options.UseEntityFramework<ApiContext>();
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnectionString"));
    options.UseRabbitMQ(opt =>
    {
        opt.HostName = builder.Configuration.GetValue<string>("RabbitMqHostName");
        opt.UserName = "deniz";
        opt.Password = "bahar0227";
        opt.ExchangeName = "ProductExchange";
    });
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<ICategoryService, CategoryService>();

// Add CORS policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder =>
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

app.UseHttpsRedirection();

app.UseAuthorization();

// Use CORS policy
app.UseCors("AllowAll");

app.MapControllers();

// CAP Dashboard'u kullan?n
app.UseCapDashboard();

app.Run();
