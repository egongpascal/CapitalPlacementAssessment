using AutoMapper;
using CapitalPlacementAssessment.Repository;
using CapitalPlacementAssessment.Repository.Implementations;
using Microsoft.Azure.Cosmos;

var builder = WebApplication.CreateBuilder(args);

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

builder.Services.AddScoped<ProgramRepository>(sp =>
{
    var cosmosClient = sp.GetService<CosmosClient>();
    var logger = sp.GetService<ILogger<ProgramRepository>>();
    var mapper = sp.GetService<IMapper>();

    return new ProgramRepository(cosmosClient, logger, mapper);
});

builder.Services.AddScoped<WorkflowRepository>(sp =>
{
    var cosmosClient = sp.GetService<CosmosClient>();
    var logger = sp.GetService<ILogger<WorkflowRepository>>();
    var mapper = sp.GetService<IMapper>();

    return new WorkflowRepository(cosmosClient, logger, mapper);
});

builder.Services.AddScoped<ApplicationTemplateRepo>(sp =>
{
    var cosmosClient = sp.GetService<CosmosClient>();
    var logger = sp.GetService<ILogger<ApplicationTemplateRepo>>();
    var mapper = sp.GetService<IMapper>();

    return new ApplicationTemplateRepo(cosmosClient, logger, mapper);
});



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
