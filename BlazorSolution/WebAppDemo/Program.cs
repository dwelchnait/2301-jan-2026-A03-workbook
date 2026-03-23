using WebAppDemo.Components;

#region Additional Namespaces
using Microsoft.EntityFrameworkCore;
using WestWindSystem.DAL;
using WestWindSystem.BLL;
#endregion

var builder = WebApplication.CreateBuilder(args);

// obtain the connection to supply to out DAL context class
// We will be registering the db connection AND our services
//      we create within the BLL folder of your library

//grab your connection string from its declared location
// either appsettings.json
//     or user secrets  

//access is first to appsettings.json
//then to user secrets
//if there is a declaration in both
// the declaration in appsettings is over ridden with the
//          declaration in user secrets.
var connectstring = builder.Configuration.GetConnectionString("WWDB");

// register the DbContext to the Sql Server
builder.Services.AddDbContext<WestWindContext>(options  => options.UseSqlServer(connectstring));

//Register your service class
//One for EACH service class in your BLL
//project recommendation, ADD lastname to the front of your service class
builder.Services.AddScoped<ProductServices>();


// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
}
app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
