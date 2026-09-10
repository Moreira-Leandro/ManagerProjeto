using Application.Service;
using Domain.Interfaces;
using Infrastructure.DAO;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
IConfigurationSection firebird = builder.Configuration.GetSection("Firebird");
string dbPath = Path.GetFullPath(
    Path.Combine(builder.Environment.ContentRootPath, "..", "Database", "GestaoEscolar.fdb"));
string connectionString =
    $"User={firebird["User"]};Password={firebird["Password"]};Database={dbPath};DataSource={firebird["DataSource"]};Port={firebird["Port"]};Dialect=3;Charset=UTF8;";

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IAlunoRepositorio>(_ => new AlunoDAO(connectionString));
builder.Services.AddScoped<AlunoService>();
builder.Services.AddScoped<ICidadeRepositorio>(_ => new CidadeDAO(connectionString));
builder.Services.AddScoped<CidadeService>();

WebApplication app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();