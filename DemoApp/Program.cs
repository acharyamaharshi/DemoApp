using DemoApp1.ENTITY;
using DemoApp1.INTERFACE;
using DemoApp1.SERVICE;
using DemoApp1.UNIT_OF_WORK.INTERFACE;
using DemoApp1.UNIT_OF_WORK.SERVICE;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


// Configuring Db Context :
builder.Services.AddDbContext<ApplicationDbContext>(options =>
           options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")
           )
           );

// Calling Seeder 
// Registering UNIT OF WORK SERVICE
builder.Services.AddScoped<IUnitOfWork,UnitOfWorkService>();
builder.Services.AddScoped<IResumeService,ResumeService>();



var app = builder.Build();
 void Configure(IApplicationBuilder app, IWebHostEnvironment env, ApplicationDbContext dbContext)
{

    // Call SeedUsers method to seed initial data
    dbContext.SeedUsers();

}
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

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
