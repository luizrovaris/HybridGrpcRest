using Microsoft.AspNetCore.Server.Kestrel.Core;
using PoC.HybridGrpcRest.Service.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddGrpc();
builder.WebHost.ConfigureKestrel(serverOptions =>
{
    serverOptions.ListenAnyIP(5001, listenOptions => listenOptions.Protocols = HttpProtocols.Http1);
    serverOptions.ListenAnyIP(5002, listenOptions => listenOptions.Protocols = HttpProtocols.Http2);
});

//Services
builder.Services.AddScoped<IStatusService, StatusService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
    app.UseSwagger();
    app.UseSwaggerUI();
//}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();

//app.UseEndpoints(endpoints =>
//{
//    endpoints.MapControllers();
//    endpoints.MapGrpcService<StatusService>();
//    endpoints.MapGet("/", async context =>
//    {
//        await context.Response.WriteAsync("Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");
//    });

//});
app.MapControllers();
app.MapGrpcService<StatusService>();
app.MapGet("/", () => "gRPC endpoints... To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();
