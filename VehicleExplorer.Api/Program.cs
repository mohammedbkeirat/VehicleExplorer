using VehicleExplorer.Api.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
    options.AddDefaultPolicy(policy =>
        policy.WithOrigins(builder.Configuration["AllowedUrls"])
              .AllowAnyMethod()
              .AllowAnyHeader()
              .AllowCredentials()
              )
    );

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddHttpClient("Nhtsa", client =>
{
    client.BaseAddress = new Uri("https://vpic.nhtsa.dot.gov/api/vehicles/");
    client.Timeout = TimeSpan.FromSeconds(60);
});

builder.Services.AddScoped<IVehicleExplorerService, VehicleExplorerService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseCors();

app.MapControllers();

app.Run();
