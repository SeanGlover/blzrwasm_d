using blzrwasm_d.Server.Services;
using blzrwasm_d.Server.Models;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

// mongodb requirements
// users
builder.Services.Configure<UserStoreDatabaseSettings>(builder.Configuration.GetSection(nameof(UserStoreDatabaseSettings)));
builder.Services.AddSingleton<IUserStoreDatabaseSettings>(sp => sp.GetRequiredService<Microsoft.Extensions.Options.IOptions<UserStoreDatabaseSettings>>().Value);
builder.Services.AddSingleton<IMongoClient>(s => new MongoClient(builder.Configuration.GetValue<string>("UserStoreDatabaseSettings:ConnectionString")));
builder.Services.AddScoped<IUserService, UserService>();
// products
builder.Services.Configure<ProductStoreDatabaseSettings>(builder.Configuration.GetSection(nameof(ProductStoreDatabaseSettings)));
builder.Services.AddSingleton<IProductStoreDatabaseSettings>(sp => sp.GetRequiredService<Microsoft.Extensions.Options.IOptions<ProductStoreDatabaseSettings>>().Value);
builder.Services.AddSingleton<IMongoClient>(s => new MongoClient(builder.Configuration.GetValue<string>("ProductStoreDatabaseSettings:ConnectionString")));
builder.Services.AddScoped<IProductService, ProductService>();

// sqlserver requirements
builder.Services.AddDbContext<AppDbContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("DBConnection_employees")));
builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseBlazorFrameworkFiles();
app.UseStaticFiles();
app.UseRouting();
app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();
