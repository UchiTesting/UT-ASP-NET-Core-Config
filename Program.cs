var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Replaces the `public void ConfigureServices(IServiceCollection services)` in Startup.cs
// from previous versions.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

Console.WriteLine($"MyKey from builder => {builder.Configuration["MyKey"]}");

var app = builder.Build();

// Configure the HTTP request pipeline.
// Replaces the `public void Configure(IApplicationBuilder app, IWebHostEnvironment env)` in Startup.cs
// from previous versions.

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.Use(async (context, next) =>
{
    Console.WriteLine($"\nMyKey from app => {app.Configuration["MyKey"]}");
    Console.WriteLine($"MyApi.Url from app => {app.Configuration["MyApi:Url"]}");
    Console.WriteLine($"MyApi.Url from app => {app.Configuration["MyApi:ApiKey"]}\n");

    await next();
});


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
