using AutoMapper;
using CapitalPlacementAssessment.Repository;
using CapitalPlacementAssessment.Repository.Implementations;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Configuration;



var builder = WebApplication.CreateBuilder(args);

var configuration = new ConfigurationBuilder()
           .SetBasePath(builder.Environment.ContentRootPath)
           .AddJsonFile("appsettings.json")
           .Build();

var cosmosDbConnectionString = configuration.GetConnectionString("CosmosDBConnection");
var cosmosClientOptions = new CosmosClientOptions
{
    ConnectionMode = ConnectionMode.Gateway, // Use appropriate connection mode
    SerializerOptions = new CosmosSerializationOptions
    {
        PropertyNamingPolicy = CosmosPropertyNamingPolicy.CamelCase
    }
};

var cosmosClient = new CosmosClient(cosmosDbConnectionString, cosmosClientOptions);

builder.Services.AddSingleton(cosmosClient);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var mapperConfig = new MapperConfiguration(cfg =>
{
    cfg.AddProfile<AutoMapperProfile>(); 
});

IMapper mapper = mapperConfig.CreateMapper();

builder.Services.AddScoped<ILogger<ProgramRepository>, Logger<ProgramRepository>>();
builder.Services.AddSingleton(mapper);

// Register repositories
builder.Services.AddScoped<IWorkflowRepository, WorkflowRepository>();
builder.Services.AddScoped<IProgramRepository, ProgramRepository>();
builder.Services.AddScoped<IApplicationTemplateRepo, ApplicationTemplateRepo>();



//builder.Services.AddScoped<ProgramRepository>(sp =>
//{
//    var cosmosClient = sp.GetService<CosmosClient>();
//    var logger = sp.GetService<ILogger<ProgramRepository>>();
//    var mapper = sp.GetService<IMapper>();

//    return new ProgramRepository(cosmosClient, logger, mapper);
//});

//builder.Services.AddScoped<WorkflowRepository>(sp =>
//{
//    var cosmosClient = sp.GetService<CosmosClient>();
//    var logger = sp.GetService<ILogger<WorkflowRepository>>();
//    var mapper = sp.GetService<IMapper>();

//    return new WorkflowRepository(cosmosClient, logger, mapper);
//});

//builder.Services.AddScoped<ApplicationTemplateRepo>(sp =>
//{
//    var cosmosClient = sp.GetService<CosmosClient>();
//    var logger = sp.GetService<ILogger<ApplicationTemplateRepo>>();
//    var mapper = sp.GetService<IMapper>();

//    return new ApplicationTemplateRepo(cosmosClient, logger, mapper);
//});

builder.Logging.AddConsole();
builder.Logging.AddDebug();



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
