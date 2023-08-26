using SPSA.API;
using SPSA.API.Helper.Middleware;
using SPSA.API.Helper.SignalR;

var builder = WebApplication.CreateBuilder(args);
var AllowOrigins = "_AllowSpecificOrigins";
ConfigurationManager configuration = builder.Configuration;
builder.Services.AddInfrastructure(configuration);
builder.Services.AddSignalR();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

#region CORS Setup

builder.Services.AddCors(options =>
{
    options.AddPolicy(AllowOrigins, builder =>
    {
        builder
        .AllowAnyOrigin()
        //.SetIsOriginAllowed(domainName => domainName.Contains("http"))
        .AllowAnyMethod()
        .AllowAnyHeader();
        //.AllowCredentials();
    });
});

#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseMiddleware<ErrorHandlerMiddleware>();

app.UseCors(AllowOrigins);
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers().RequireAuthorization();

app.MapHub<PushNotificationHub>("/signalr/push-notification", option =>
{
    // option.Transports = Microsoft.AspNetCore.Http.Connections.HttpTransportType.ServerSentEvents;
});

app.Run();
