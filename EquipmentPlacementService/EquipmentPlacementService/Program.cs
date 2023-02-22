using EquipmentPlacementService.Data;
using EquipmentPlacementService.Infrastructure.Extensions;
using EquipmentPlacementService.Infrastructure.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services
    .AddDatabase(builder.Configuration)
    .AddApplicationServices()
    .AddSwagger();

var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    using (var scope = app.Services.CreateScope())
    {
        var initialiser = scope.ServiceProvider.GetRequiredService<ApplicationDbContextInitialiser>();
        await initialiser.SeedAsync();
    }
}

app.UseHttpsRedirection();

app.UseMiddleware<ApiKeyMiddleware>(builder.Configuration);
app.UseAuthorization();

app.MapControllers();

app.Run();
