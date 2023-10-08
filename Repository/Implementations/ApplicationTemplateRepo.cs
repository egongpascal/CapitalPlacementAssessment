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
        private readonly IConfiguration _config;
        private readonly string _cid;
        private readonly string _db;
        private readonly string _pk;

        public ApplicationTemplateRepo(CosmosClient cosmosClient, ILogger<ApplicationTemplateRepo> logger, IMapper mapper, IConfiguration configuration1)
        {
            _logger = logger;
            _mapper = mapper;
            _cosmosClient = cosmosClient;
            _config = configuration1;
            _db = _config["ConnectionStrings:Database"];
            _cid = _config["ConnectionStrings:ContainerId"];
            _pk = _config["ConnectionStrings:PartitionKey"];
        }
        public async Task<ResponseClass<ApplicationTemplateDto>> CreateApplicationTemplate(ApplicationTemplateDto request)
        {
            var result = new ResponseClass<ApplicationTemplateDto>();
            var newAppTemp = _mapper.Map<ApplicationTemplate>(request);

            try
            {
                var container = _cosmosClient.GetContainer(_db, _cid);
                var partitionKey = new PartitionKey(_pk);
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
                var container = _cosmosClient.GetContainer(_db, _cid);
                var partitionKey = new PartitionKey(_pk);
                var appTemp = await container.ReadItemAsync<ApplicationTemplate>(programId, partitionKey);
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
            var update = _mapper.Map<ApplicationTemplate>(request);
            try
            {
                var container = _cosmosClient.GetContainer(_db, _cid);
                var partitionKey = new PartitionKey(_pk);
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
