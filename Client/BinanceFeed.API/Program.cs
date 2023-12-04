using BinanceFeed.API;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDependencies(builder.Configuration);

builder.Services
	.AddControllers()
	.AddXmlSerializerFormatters();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseSwagger();

app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseMiddleware<ErrorHandlingMiddleware>();

app.UseAuthorization();

app.UseOutputCache();

app.MapControllers();

app.Run();

//As part of the Integration testing WebApplicationFactory<TProgram> class requires a public Program class
public partial class Program { }