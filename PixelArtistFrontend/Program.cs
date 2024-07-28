var builder = WebApplication.CreateBuilder(args);

// Ensure that the configuration is loaded correctly
var configuration = builder.Configuration;

// Add services to the container.
builder.Services.AddControllersWithViews();

var baseAddress = configuration.GetValue<string>("BackendApi:BaseAddress");
Console.WriteLine($"Base Address: {baseAddress}");  // Debug line to check the value

if (string.IsNullOrEmpty(baseAddress))
{
    throw new InvalidOperationException("Base address for BackendClient is not configured.");
}

builder.Services.AddHttpClient("BackendClient", client =>
{
    client.BaseAddress = new Uri(baseAddress);
}).ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
{
    ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; }
});

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
