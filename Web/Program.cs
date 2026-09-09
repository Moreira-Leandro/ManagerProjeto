using Application.Interfaces;
using Application.Service;
using Infrastructure.DAO;

var builder = WebApplication.CreateBuilder(args);
var fb = builder.Configuration.GetSection("Firebird");
var dbPath = Path.GetFullPath(
    Path.Combine(builder.Environment.ContentRootPath, "..", "Database", "GestaoEscolar.fdb"));
var connectionString =
    $"User={fb["User"]};Password={fb["Password"]};Database={dbPath};DataSource={fb["DataSource"]};Port={fb["Port"]};Dialect=3;Charset=UTF8;";

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IAlunoRepository>(_ => new AlunoDAO(connectionString));
builder.Services.AddScoped<AlunoService>();
builder.Services.AddScoped<ICidadeRepository>(_ => new CidadeDAO(connectionString));
builder.Services.AddScoped<CidadeService>();

var app = builder.Build();

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