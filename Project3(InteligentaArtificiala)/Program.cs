using Microsoft.EntityFrameworkCore;
using Project3_InteligentaArtificiala_.Helper;
using Project3_InteligentaArtificiala_.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddScoped<CalculatingMINorMaxForEachColumn>();
builder.Services.AddScoped<NormalizeData>();
builder.Services.AddScoped<SplittingTheTableInTwoParts>();
builder.Services.AddScoped<SettingXByCalculatingGINAndActivation>();

builder.Services.AddDbContext<GlassContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddDbContext<NormalizeContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddDbContext<TestingContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddDbContext<TrainingContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Glass}/{action=Index}/{id?}");

app.Run();
