using Portfolio_API.Data;
using Microsoft.Data.SqlClient;
using Portfolio_API.Services;
using System.Text.Json.Serialization;

var  MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCors(options=>
{
    options.AddPolicy(MyAllowSpecificOrigins,
                    policy=>
                    {
                        policy.WithOrigins("http://127.0.0.1:5264");
                    });
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers()
        .AddJsonOptions(options=>
        {
            options.JsonSerializerOptions.ReferenceHandler=ReferenceHandler.IgnoreCycles;
            options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
        });

var cn = new SqlConnectionStringBuilder();
cn.ServerSPN="db\\MSSQLSERVER";
cn.UserID = "sa";
cn.Password="P@ssw0rd";
cn.InitialCatalog="ManagedItems";
cn.TrustServerCertificate = true;
cn.PersistSecurityInfo = false;

builder.Services.AddSqlServer<ItemContext>(
    cn.ConnectionString,
    null,
    options=>
    {
        options.EnableSensitiveDataLogging(true);
    }
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

app.UseCors(MyAllowSpecificOrigins);

app.UseAuthorization();

app.MapControllers();

app.Run();
