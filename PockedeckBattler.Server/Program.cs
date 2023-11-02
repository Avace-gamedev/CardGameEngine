using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using NSwag;
using PockedeckBattler.Server.SignalR;
using PockedeckBattler.Server.SignalR.Combats;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddCors();
builder.Services.AddControllers().AddNewtonsoftJson(options => options.SerializerSettings.Converters.Add(new StringEnumConverter(new CamelCaseNamingStrategy())));
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
builder.Services.AddSignalR();

builder.Services.AddSingleton<IHubConnections, HubConnectionsInMemory>();

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

app.Run();
