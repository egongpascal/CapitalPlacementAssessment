using CapitalPlacementAssessment.Repository.Implementations;
using CapitalPlacementAssessment.Services.Implementation;
using CapitalPlacementAssessment.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IProgramRepository, ProgramRepository>();
builder.Services.AddScoped<IApplicationTemplateRepo, ApplicationTemplateRepo>();
builder.Services.AddScoped<IWorkflowRepository, WorkflowRepository>();

builder.Services.AddScoped<IProgramService, ProgramService>();
builder.Services.AddScoped<IWorkflowService, WorkflowService>();
builder.Services.AddScoped<IApplicationTemplateService, ApplicationTemplateService>();


builder.Services.AddScoped<IApplicationTemplateRepo, ApplicationTemplateRepo>();
builder.Services.AddScoped<IWorkflowRepository, WorkflowRepository>();

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
