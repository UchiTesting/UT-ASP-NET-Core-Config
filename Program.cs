using ASP;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Replaces the `public void ConfigureServices(IServiceCollection services)` in Startup.cs
// from previous versions.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<MyApiOptions>(builder.Configuration.GetSection(MyApiOptions.MyApi));

Console.WriteLine($"MyKey from builder => {builder.Configuration["MyKey"]}");

// You can put it as soon as builder is declared. Where it is placed is important.
// Putting it last allows to use other providers at startup while having it overriding values after.
builder.Configuration.AddJsonFile("MyConfig.json", false, true);

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

    var apiOptions = app.Configuration.GetSection(MyApiOptions.MyApi).Get<MyApiOptions>();

    Console.WriteLine($"MyApi.Url from app => {apiOptions.Url}");
    Console.WriteLine($"MyApi.ApiKey from app => {apiOptions.ApiKey}\n");

    await next();
});


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
