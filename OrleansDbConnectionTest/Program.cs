using Orleans;
using Orleans.Configuration;
using Orleans.Hosting;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Host.UseOrleans(options =>
{
    options.Configure((Action<ClusterOptions>)(o =>
    {
        o.ClusterId = "dev";
        o.ServiceId = "dev";
    }));
    options.UseAdoNetClustering(x =>
    {
        x.Invariant = "Npgsql";
        x.ConnectionString = builder.Configuration.GetConnectionString("DbConnectionString");
    });
    options.AddAdoNetGrainStorage("orleansStorage", x =>
    {
        x.Invariant = "Npgsql";
        x.ConnectionString = builder.Configuration.GetConnectionString("DbConnectionString");
    });
    options.ConfigureEndpoints
    (
        siloPort: 11111,
        gatewayPort: 30000,
        listenOnAnyHostAddress: true
    );
    options.UseDashboard(options =>
    {
        options.Username = "USERNAME";
        options.Password = "PASSWORD";
        options.Host = "*";
        options.Port = 8080;
        options.HostSelf = true;
        options.CounterUpdateIntervalMs = 1000;
    });
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
