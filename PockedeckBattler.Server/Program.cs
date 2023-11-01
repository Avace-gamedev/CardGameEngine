using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using NSwag;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Add services to the container.

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
builder.Services.AddCors();

WebApplication app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseOpenApi();
    app.UseSwaggerUi3();

    app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
