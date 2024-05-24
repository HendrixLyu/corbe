using Microsoft.AspNetCore.Http.Json;

var builder = WebApplication.CreateBuilder(args);


builder.Services.Configure<JsonOptions>(options =>
{
    options.SerializerOptions.PropertyNamingPolicy = null;
});

builder.Services.AddEndpointsApiExplorer();

//HINT: dependency injection
builder.Services.AddScoped<ISecretKeyService, SecretKeyService>();
builder.Services.AddScoped<IBasicInfoService, BasicInfoService>();
builder.Services.AddScoped<ICertLocationProvider, CertLocationProvider>();

var app = builder.Build();

//map default endpoint
app.MapGet("/Home", () =>
{
    return Results.Content("<html><body><h1>This is not the endpoint you're looking for!</h1></body></html>", "text/html");
});

//map decoy endpoint
app.MapGet("/Decoy", () =>
{
    return new InfoResult()
    {
        SystemOS = "DECOY",
        CertificateHash = "DECOY"
    };
});

//map info endpoint
app.MapGet("/Info", (ISecretKeyService keyService, IBasicInfoService infoService) =>
{
    return new InfoResult()
    {
        SystemOS = infoService.GetOperatingSystem(),
        CertificateHash = keyService.GetCertHash()
    };
});

app.UseRouting();
app.UseEndpoints(x => { });

app.UseSpa(spa =>
{
    spa.Options.SourcePath = "UI";
    if (app.Environment.IsDevelopment())
    {
        spa.UseProxyToSpaDevelopmentServer("http://localhost:3001");
    }
});

app.Run();
