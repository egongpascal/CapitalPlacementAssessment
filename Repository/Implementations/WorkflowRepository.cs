using AutoMapper;
using CapitalPlacementAssessment.Domain;
using CapitalPlacementAssessment.Domain.DTOs;
using CapitalPlacementAssessment.Models;
using Microsoft.Azure.Cosmos;
using System.Net;

namespace CapitalPlacementAssessment.Repository.Implementations
{
    public class WorkflowRepository : IWorkflowRepository
    {
        private readonly CosmosClient _cosmosClient;
        private readonly ILogger<WorkflowRepository> _logger;
        private readonly IMapper _mapper;

        public WorkflowRepository(CosmosClient cosmosClient, ILogger<WorkflowRepository> logger, IMapper mapper)
        {
            _cosmosClient = cosmosClient;
            _logger = logger;
            _mapper = mapper;
        }
        public async Task<ResponseClass<WorkflowDto>> CreateWorkFlow(WorkflowDto request)
        {
            var result = new ResponseClass<WorkflowDto>();
            var newWorkflow = _mapper.Map<WorkFlow>(request);

            try
            {
                var container = _cosmosClient.GetContainer("TestDB", "Container1");
                var partitionKey = new PartitionKey(newWorkflow.ProgramId);
                var response = await container.CreateItemAsync(newWorkflow, partitionKey);

                if (response.StatusCode == HttpStatusCode.Created)
                {
                    _logger.LogInformation("workflow created successfully.");
                    result.Sucesss = true;
                    result.Message = "workflow created successfully";
                    return result;
                }
                else
                {
                    _logger.LogError("Failed to create v.");
                    result.Sucesss = false;
                    result.Message = "Failed to create workflow.";
                    return result;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating  Application Template.");
                throw;
            }
        }
        public async Task<ResponseClass<WorkflowDto>> GetWorkFlow(string programId)
        {
            var result = new ResponseClass<WorkflowDto>();
            try
            {
                var container = _cosmosClient.GetContainer("TestDB", "Container1");
                var workflow = await container.ReadItemAsync<WorkFlow>(programId, new PartitionKey(programId));
                if (workflow != null)
                {
                    _logger.LogInformation("fetched successfully.");
                    var mapResult = _mapper.Map<WorkflowDto>(workflow);
                    result.Data = mapResult;
                    result.Sucesss = true;
                    result.Message = "Fetch successful";

                    return result;
                }

                _logger.LogError("Failed to fetch workflow or not found.");

                result.Sucesss = true;
                result.Message = "Failed to fetch workflow or not found.";

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching workflow.");
                throw;
            }

        }

        public async Task<ResponseClass<WorkflowDto>> UpdateWorkFlow(WorkflowDto request)
        {
            var result = new ResponseClass<WorkflowDto>();
            var update = _mapper.Map<WorkFlow>(request);
            try
            {
                var container = _cosmosClient.GetContainer("TestDB", "Container1");
                var partitionKey = new PartitionKey(update.ProgramId);
                var response = await container.ReplaceItemAsync(update, update.ProgramId, partitionKey);

                if (response.StatusCode == HttpStatusCode.NoContent)
                {
                    _logger.LogInformation("workflow updated successfully.");
                    result.Sucesss = true;
                    result.Message = "workflow updated successfully.";
                    return result;
                }
                else
                {
                    _logger.LogError("Failed to update workflow.");
                    result.Sucesss = false;
                    result.Message = "Failed to update workflow.";
                    return result;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating workflow.");
                throw;
            }
        }
    }
}
