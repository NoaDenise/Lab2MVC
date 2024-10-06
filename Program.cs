var builder = WebApplication.CreateBuilder(args);

// Tjänster till containern
builder.Services.AddControllersWithViews();
builder.Services.AddHttpClient();
builder.Services.AddSession(); // För sessionshantering

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseSession(); // Aktivera session
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
