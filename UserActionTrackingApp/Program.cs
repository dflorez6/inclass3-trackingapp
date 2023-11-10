/*  Program.cs
    In Class 1

    Revision History
    David Florez ID: 8820815, 2023.11.09: Created
*/
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// To work with sessions/cookies
// DO THIS FIRST BEFORE CREATING THE APP
// Sessions by default live for 20 minutes
// builder.Services.AddSession(); // Default Session
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(5); // App idle for 5 minute, session is destroyed
});
builder.Services.AddMemoryCache();

// TODO: Still undecided about the approach for this inClass
// To pass session directly in the views. Allows a view to talk directly to the Session
// builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

// Sessions Implementation. MUST COME BEFORE ROUTES
app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
