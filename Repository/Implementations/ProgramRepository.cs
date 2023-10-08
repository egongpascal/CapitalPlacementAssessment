using AutoMapper;
using CapitalPlacementAssessment.Domain;
using CapitalPlacementAssessment.Domain.DTOs;
using CapitalPlacementAssessment.Models;
using Microsoft.Azure.Cosmos;
using System.Net;

namespace CapitalPlacementAssessment.Repository.Implementations
{
    public class ProgramRepository : IProgramRepository
    {
        private readonly CosmosClient _cosmosClient;
        private readonly ILogger<ProgramRepository> _logger;
        private readonly IMapper _mapper;



        public ProgramRepository(ILogger<ProgramRepository> logger, IMapper mapper, CosmosClient cosmosClient)
        {
            _logger = logger;
            _mapper = mapper;
            _cosmosClient = cosmosClient;
        }

        public async Task<ResponseClass<ProgramDetailsDto>> GetProgram(string id)
        {
            var result = new ResponseClass<ProgramDetailsDto>();
            try
            {
                var container = _cosmosClient.GetContainer("TestDB", "Container1");
                var program = await container.ReadItemAsync<ProgramDetails>(id.ToString(), new PartitionKey(id.ToString()));
                if (program != null)
                {
                    _logger.LogInformation("Program fetched successfully.");
                    var mapResult = _mapper.Map<ProgramDetailsDto>(program);
                    result.Data = mapResult;
                    result.Sucesss = true;
                    result.Message = "Fetch successful";

                    return result;
                }

                _logger.LogError("Failed to fetch program.");

                result.Sucesss = true;
                result.Message = "Fetch Failed";

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching program.");
                throw;
            }
            
        }

        public async Task<ResponseClass<ProgramDetailsDto>> CreateProgram(ProgramDetailsDto program)
        {
            var result = new ResponseClass<ProgramDetailsDto>();
            var newProgram = _mapper.Map<ProgramDetails>(program);

            try
            {
                var container = _cosmosClient.GetContainer("TestDB", "Container1");
                var partitionKey = new PartitionKey("/TenantId");
                var response = await container.CreateItemAsync(newProgram, partitionKey);

                if (response.StatusCode == HttpStatusCode.Created)
                {
                    _logger.LogInformation("Program created successfully.");
                    result.Sucesss = true;
                    result.Message = "Program created successfully";
                    return result;
                }
                else
                {
                    _logger.LogError("Failed to create the program.");
                    result.Sucesss = false;
                    result.Message = "Failed to create the program.";
                    return result;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating the program.");
                throw;
            }
        }

        public async Task<ResponseClass<ProgramDetailsDto>> UpdateProgram(ProgramDetailsDto program)
        {
            var result = new ResponseClass<ProgramDetailsDto>();
            var update = _mapper.Map<ProgramDetails>(program);
            try
            {
                var container = _cosmosClient.GetContainer("TestDB", "Container1");
                var partitionKey = new PartitionKey("/TenantId");
                var response = await container.ReplaceItemAsync(update, update.id, partitionKey);

                if (response.StatusCode == HttpStatusCode.NoContent)
                {
                    _logger.LogInformation("Program updated successfully.");
                    result.Sucesss = true;
                    result.Message = "Program updated successfully.";
                    return result;
                }
                else
                {
                    _logger.LogError("Failed to update the program.");
                    result.Sucesss = false;
                    result.Message = "Failed to update the program.";
                    return result;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating the program.");
                throw;
            }
        }

        public async Task<ResponseClass<PreviewDto>> GetPreview(string programId)
        {
            var result = new ResponseClass<PreviewDto>();
            try
            {
                var container = _cosmosClient.GetContainer("TestDB", "Container1");
                var program = await container.ReadItemAsync<ProgramDetails>(programId, new PartitionKey(programId));
                var res = program.Resource;
                if (program != null)
                {
                    _logger.LogInformation("Preview fetched successfully.");
                    var mapResult = new PreviewDto()
                    {
                        ProgramSummary = res.ProgramSummary,
                        ProgramDescription = res.ProgramDescription,
                        KeySkills = res.KeySkills,
                        ProgramBenefits = res.ProgramBenefits,
                        ApplicationCriterias = res.ApplicationCriterias
                    };

                    result.Data = mapResult;
                    result.Sucesss = true;
                    result.Message = "Fetch successful";

                    return result;
                }

                _logger.LogError("Failed to fetch preview.");

                result.Sucesss = true;
                result.Message = "Fetch Failed";

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching preview.");
                throw;
            }

        }

    }
}
