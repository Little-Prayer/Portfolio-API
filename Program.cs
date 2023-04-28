using Portfolio_API.Data;
using Microsoft.Data.SqlClient;
using Portfolio_API.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var cn = new SqlConnectionStringBuilder();
cn.ServerSPN="db\\MSSQLSERVER";
cn.UserID = "sa";
cn.Password="P@ssw0rd";
cn.InitialCatalog="ManagedItems";
cn.TrustServerCertificate = true;
cn.PersistSecurityInfo = false;

builder.Services.AddSqlServer<ItemContext>(
    cn.ConnectionString
);

builder.Services.AddScoped<ItemService>();
builder.Services.AddScoped<EventService>();
builder.Services.AddScoped<CategoryService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
