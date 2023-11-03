using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using NSwag;
using PockedeckBattler.Server.Middlewares.Exceptions;
using PockedeckBattler.Server.Rest.Combats;
using PockedeckBattler.Server.Rest.Exceptions;
using PockedeckBattler.Server.SignalR;
using PockedeckBattler.Server.SignalR.Combats;
using PockedeckBattler.Server.SignalR.Combats.Notifications;
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

builder.Services.AddProblemDetails();

builder.Services.AddSingleton<IWebApiExceptionHandler, GameEngineExceptionHandler>();
builder.Services.AddSingleton<ExceptionMiddleware>();

builder.Services.AddSignalR().AddNewtonsoftJsonProtocol(options => ConfigureNewtonsoft(options.PayloadSerializerSettings));
builder.Services.AddMediatR(options => { options.RegisterServicesFromAssemblyContaining<Program>(); });

builder.Services.AddSingleton<IHubConnections, HubConnectionsInMemory>();

builder.Services.AddSingleton<IHubConnections, HubConnectionsInMemory>();

builder.Services.AddSingleton<IStore<CombatInstanceWithMetadata>, MemoryStore<CombatInstanceWithMetadata>>();
builder.Services.AddSingleton<ICombatService, CombatsService>();

builder.Services.AddSingleton<IStore<CombatInPreparation>, CombatInPreparationFileStore>();
builder.Services.AddSingleton<ICombatInPreparationService, CombatInPreparationService>();

builder.Services.AddSingleton<PublishCombatNotificationToSignalRClients>(
    provider => new PublishCombatNotificationToSignalRClients(provider, TimeSpan.FromSeconds(0.5))
);
builder.Services.AddHostedService<PublishCombatNotificationToSignalRClients>(provider => provider.GetRequiredService<PublishCombatNotificationToSignalRClients>());

WebApplication app = builder.Build();

app.UseExceptionHandler();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseOpenApi();
    app.UseSwaggerUi3();

    app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin().DisallowCredentials());
}

app.UseMiddleware<ExceptionMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapHub<CombatsHub>("/signalr/combats");

ILogger<Program>? log = app.Services.GetService<ILogger<Program>>();

log?.LogInformation("App is starting");

app.Run();

log?.LogInformation("Last log before exit");

return;

void ConfigureNewtonsoft(JsonSerializerSettings settings)
{
    settings.Converters.Add(new StringEnumConverter(new CamelCaseNamingStrategy()));
}
