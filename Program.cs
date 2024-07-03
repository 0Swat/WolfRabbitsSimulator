// Tworzenie i konfiguracja aplikacji
var builder = WebApplication.CreateBuilder(args);
// Rejestracja SimulationManager jako singleton
builder.Services.AddSingleton<SimulationManager>();

var app = builder.Build();

// Konfiguracja obs�ugi b��d�w i HSTS dla �rodowiska produkcyjnego
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

// Konfiguracja middleware
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseWebSockets();

// Mapowanie g��wnego endpointu do pliku index.html
app.MapGet("/", async context =>
{
    await context.Response.SendFileAsync(Path.Combine(app.Environment.WebRootPath, "index.html"));
});

// Mapowanie trasy WebSocket
app.MapWebSocketRoute();

// Uruchomienie aplikacji
app.Run();

// Rozszerzenie do obs�ugi WebSocket
public static class WebSocketExtensions
{
    public static IApplicationBuilder MapWebSocketRoute(this WebApplication app)
    {
        app.Use(async (context, next) =>
        {
            if (context.Request.Path == "/ws")
            {
                if (context.WebSockets.IsWebSocketRequest)
                {
                    // Akceptacja po��czenia WebSocket i przekazanie do SimulationManager
                    using var webSocket = await context.WebSockets.AcceptWebSocketAsync();
                    var simulationManager = context.RequestServices.GetRequiredService<SimulationManager>();
                    await simulationManager.HandleWebSocketConnection(webSocket);
                }
                else
                {
                    context.Response.StatusCode = 400;
                }
            }
            else
            {
                await next();
            }
        });

        return app;
    }
}