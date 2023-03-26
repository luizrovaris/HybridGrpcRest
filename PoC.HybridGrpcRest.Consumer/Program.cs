using Microsoft.Net.Http.Headers;
using System.Net;
using System.Net.Mime;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHttpClient("MyHttpClient", clientConfig =>
{
    clientConfig.BaseAddress = new Uri("http://localhost:5001");
    clientConfig.DefaultRequestHeaders.Add(HeaderNames.Accept, MediaTypeNames.Application.Json);
})
    .ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
    {
        AllowAutoRedirect = false,
        UseDefaultCredentials = true,
        AutomaticDecompression = DecompressionMethods.None,
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
