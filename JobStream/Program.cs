using JobStream.Extensions;
using JobStream.Seed;
using JobStream.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddApplicationService(builder.Configuration);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseMiddleware<ExceptionMiddleware>();
if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.UseRouting();
app.UseCors(options => options.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());
app.UseAuthorization();
app.UseDefaultFiles();
app.UseStaticFiles();
app.MapControllers();
app.MapFallbackToController("Index", "Fallback");


//Seed
using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;
try
{
  var seed = services.GetService<SeedDataService>();
  await seed.SeedDatabase();
}
catch (Exception ex)
{
  var logger = services.GetRequiredService<ILogger<Program>>();
  logger.LogError(ex, "An error occurred during migration");
}

//Run application
app.Run();
