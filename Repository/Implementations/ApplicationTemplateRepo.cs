using AutoMapper;
using CapitalPlacementAssessment.Domain;
using CapitalPlacementAssessment.Domain.DTOs;
using CapitalPlacementAssessment.Models;
using Microsoft.Azure.Cosmos;
using System.Net;

namespace CapitalPlacementAssessment.Repository.Implementations
{
    public class ApplicationTemplateRepo : IApplicationTemplateRepo
    {
        private readonly CosmosClient _cosmosClient;
        private readonly ILogger<ApplicationTemplateRepo> _logger;
        private readonly IMapper _mapper;

        public ApplicationTemplateRepo(CosmosClient cosmosClient, ILogger<ApplicationTemplateRepo> logger, IMapper mapper)
        {
            _cosmosClient = cosmosClient;
            _logger = logger;
            _mapper = mapper;
        }
        public async Task<ResponseClass<ApplicationTemplateDto>> CreateApplicationTemplate(ApplicationTemplateDto request)
        {
            var result = new ResponseClass<ApplicationTemplateDto>();
            var newAppTemp = _mapper.Map<ApplicationTemplate>(request);

            try
            {
                var container = _cosmosClient.GetContainer("your-database-id", "your-container-id");
                var partitionKey = new PartitionKey(newAppTemp.ProgramId);
                var response = await container.CreateItemAsync(newAppTemp, partitionKey);

                if (response.StatusCode == HttpStatusCode.Created)
                {
                    _logger.LogInformation("Application Template created successfully.");
                    result.Sucesss = true;
                    result.Message = "Application Template created successfully";
                    return result;
                }
                else
                {
                    _logger.LogError("Failed to create Application Template.");
                    result.Sucesss = false;
                    result.Message = "Failed to create Application Template.";
                    return result;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating  Application Template.");
                throw;
            }
        }
        public async Task<ResponseClass<ApplicationTemplateDto>> GetApplicationTemplate(string programId)
        {
            var result = new ResponseClass<ApplicationTemplateDto>();
            try
            {
                var container = _cosmosClient.GetContainer("your-database-id", "your-container-id");
                var appTemp = await container.ReadItemAsync<ApplicationTemplate>(programId, new PartitionKey(programId));
                if (appTemp != null)
                {
                    _logger.LogInformation(" fetched successfully.");
                    var mapResult = _mapper.Map<ApplicationTemplateDto>(appTemp);
                    result.Data = mapResult;
                    result.Sucesss = true;
                    result.Message = "Fetch successful";

                    return result;
                }

                _logger.LogError("Failed to fetch template or not found.");

                result.Sucesss = true;
                result.Message = "Failed to fetch template or not found.";

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching template.");
                throw;
            }

        }

        public async Task<ResponseClass<ApplicationTemplateDto>> UpdateApplicationTemplate(ApplicationTemplateDto request)
        {
            var result = new ResponseClass<ApplicationTemplateDto>();
            var update = _mapper.Map<ProgramDetails>(request);
            try
            {
                var container = _cosmosClient.GetContainer("your-database-id", "your-container-id");
                var partitionKey = new PartitionKey(update.ProgramId);
                var response = await container.ReplaceItemAsync(update, update.ProgramId, partitionKey);

                if (response.StatusCode == HttpStatusCode.NoContent)
                {
                    _logger.LogInformation("Application Template updated successfully.");
                    result.Sucesss = true;
                    result.Message = "Application Template updated successfully.";
                    return result;
                }
                else
                {
                    _logger.LogError("Failed to update Application Template.");
                    result.Sucesss = false;
                    result.Message = "Failed to update Application Template.";
                    return result;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating Application Template.");
                throw;
            }
        }
    }
}
