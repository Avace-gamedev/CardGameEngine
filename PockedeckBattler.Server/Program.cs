using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using NSwag;
using PockedeckBattler.Server.SignalR;
using PockedeckBattler.Server.SignalR.Combats;
using PockedeckBattler.Server.Stores;
using PockedeckBattler.Server.Stores.Combats;
using PockedeckBattler.Server.Stores.CombatsInPreparation;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddCors();
builder.Services.AddControllers().AddNewtonsoftJson(options => ConfigureNewtonsoft(options.SerializerSettings));
builder.Services.AddSwaggerDocument(
    config =>
    {
        config.PostProcess = document =>
        {
            document.Info.Version = "v1";
            document.Info.Title = "PockeDeck Battler API";
            document.Info.Description = "";
            document.Info.Contact = new OpenApiContact
            {
                Name = "Ismail Bennani",
                Email = "contact@ismailbennani.fr",
                Url = "http://www.ismailbennani.fr"
            };
        };
    }
);
builder.Services.AddSignalR().AddNewtonsoftJsonProtocol(options => ConfigureNewtonsoft(options.PayloadSerializerSettings));
builder.Services.AddMediatR(options => { options.RegisterServicesFromAssemblyContaining<Program>(); });

builder.Services.AddSingleton<IHubConnections, HubConnectionsInMemory>();

builder.Services.AddSingleton<ICombatService, CombatsMemoryStore>();

builder.Services.AddSingleton<IStore<CombatInPreparation>, CombatInPreparationFileStore>();
builder.Services.AddSingleton<ICombatInPreparationService, CombatInPreparationService>();

WebApplication app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseOpenApi();
    app.UseSwaggerUi3();

    app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin().DisallowCredentials());
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapHub<CombatsHub>("/signalr/combats");

ILogger<Program>? log = app.Services.GetService<ILogger<Program>>();

log?.LogInformation("App is starting");

app.Run();

log?.LogInformation("App is exiting");

return;

void ConfigureNewtonsoft(JsonSerializerSettings settings)
{
    settings.Converters.Add(new StringEnumConverter(new CamelCaseNamingStrategy()));
}
