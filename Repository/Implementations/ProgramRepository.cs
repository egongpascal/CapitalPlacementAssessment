using CapitalPlacementAssessment.Models;
using Microsoft.Azure.Cosmos;
using System.Net;

namespace CapitalPlacementAssessment.Repository.Implementations
{
    public class ProgramRepository : IProgramRepository
    {
        private readonly CosmosClient _cosmosClient;
        private readonly ILogger<ProgramRepository> _logger;


        public ProgramRepository(CosmosClient cosmosClient, ILogger<ProgramRepository> logger)
        {
            _cosmosClient = cosmosClient;
            _logger = logger;

        }

        public async Task<ProgramDetails> GetProgram(int id)
        {
            var container = _cosmosClient.GetContainer("your-database-id", "your-container-id");
            var program = await container.ReadItemAsync<ProgramDetails>(id.ToString(), new PartitionKey(id.ToString()));
            return program;
        }


        public async Task<List<ProgramDetails>> GetAllPrograms()
        {
            try
            {
                var container = _cosmosClient.GetContainer("your-database-id", "your-container-id");
                var query =  container.GetItemQueryIterator<ProgramDetails>();
                if(query != null)
                {
                    var programs = new List<ProgramDetails>();
                    while (query.HasMoreResults)
                    {
                        var response = await query.ReadNextAsync();
                        programs.AddRange(response.ToList());
                        _logger.LogInformation("Programs retrieved successfully.");

                    }
                    return programs;
                }
                else
                {
                    _logger.LogError("Failed to fetch the program. or program not found");
                    return null;
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating the program.");
                throw;
            }
        }


        public async Task<ProgramDetails> CreateProgram(ProgramDetails program)
        {
            try
            {
                var container = _cosmosClient.GetContainer("your-database-id", "your-container-id");
                var partitionKey = new PartitionKey(program.ProgramId.ToString());
                var response = await container.CreateItemAsync(program, partitionKey);

                if (response.StatusCode == HttpStatusCode.Created)
                {
                    _logger.LogInformation("Program created successfully.");
                    return program;
                }
                else
                {
                    _logger.LogError("Failed to create the program.");
                    throw new Exception("Failed to create the program.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating the program.");
                throw;
            }
        }


        public async Task<ProgramDetails> UpdateProgram(ProgramDetails program)
        {
            try
            {
                var container = _cosmosClient.GetContainer("your-database-id", "your-container-id");
                var partitionKey = new PartitionKey(program.ProgramId.ToString());
                var response = await container.ReplaceItemAsync(program, program.ProgramId.ToString(), partitionKey);

                if (response.StatusCode == HttpStatusCode.NoContent)
                {
                    _logger.LogInformation("Program updated successfully.");
                    return program;
                }
                else
                {
                    _logger.LogError("Failed to update the program.");
                    throw new Exception("Failed to update the program.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating the program.");
                throw;
            }
        }



        public async Task DeleteProgram(int id)
        {
            var container = _cosmosClient.GetContainer("your-database-id", "your-container-id");

            // Ensure the PartitionKey value is correct for the item you want to delete
            var partitionKey = new PartitionKey("programs");

            await container.DeleteItemAsync<Program>(id.ToString(), partitionKey);
        }

    }
}
