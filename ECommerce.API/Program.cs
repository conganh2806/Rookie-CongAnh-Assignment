using ECommerce.API.Middleware;
using ECommerce.Application;
using ECommerce.Application.Settings;
using ECommerce.Infrastructure;
using ECommerce.Infrastructure.Extensions;
using ECommerce.Infrastructure.Persistence;
using ECommerce.JsonNamingPolicy;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCustomIdentity(builder.Configuration);
builder.Services.AddJWTAuthentication(builder.Configuration);
builder.Services.AddCustomAuthorization(builder.Configuration);

builder.Services.Configure<JwtSettings>(
    builder.Configuration.GetSection("Jwt"));

builder.Services.AddApplication();      
builder.Services.AddInfrastructure(builder.Configuration); 
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        // Use snake_case for both requests and responses
        options.JsonSerializerOptions.PropertyNamingPolicy = SnakeCaseNamingPolicy.Instance;
        options.JsonSerializerOptions.DictionaryKeyPolicy = SnakeCaseNamingPolicy.Instance;
        options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
        // options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    });

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerDocumentation();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();



using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    await SeedData.Initialize(services);
}

app.Run();