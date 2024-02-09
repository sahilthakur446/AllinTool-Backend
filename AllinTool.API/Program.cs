using AllinTool.Data.Context;
using AllinTool.Data.Mappers;
using AllinTool.Data.Repository.Abstract;
using AllinTool.Data.Repository.Implementation;
using AllinTool.Data.Repository.Implementations;
using AllinTool.Data.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IUnitConverter, UnitConverter>();
builder.Services.AddScoped<IBankDetailRepository, BankDetailRepository>();
builder.Services.AddScoped<IGeographicRepository, GeographicRepository>();
builder.Services.AddScoped<ITimezoneConverter, TimezoneConverter>();

// Add CORS services.
builder.Services.AddCors(options =>
{
    options.AddPolicy("MyPolicy", builder =>
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

// Use CORS policy.
app.UseCors("MyPolicy");

app.UseAuthorization();

app.MapControllers();

app.Run();
