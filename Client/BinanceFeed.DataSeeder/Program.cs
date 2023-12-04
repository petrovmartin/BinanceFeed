using BinanceFeed.DataSeeder;
using BinanceFeed.Infrastructure.SQL;

var builder = Host.CreateApplicationBuilder(args);

var msSqlConfig = builder.Configuration["MsSqlDbConfig:ConnectionString"];
builder.Services.AddMsSql(msSqlConfig);

builder.Services.AddHostedService<Seeder>();

var host = builder.Build();

await host.Services.RunDatabaseMigrations();

host.Run();
