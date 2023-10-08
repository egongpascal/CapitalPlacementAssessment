using CapitalPlacementAssessment.Repository.Implementations;
using CapitalPlacementAssessment.Services.Interfaces;

namespace CapitalPlacementAssessment.Services.Implementation
{
    public class ProgramService : IProgramService
    {
        private readonly ILogger _logger;
        private readonly IProgramRepository _proRepo;
        public ProgramService(ILogger<ProgramService> logger, IProgramRepository programRepository)
        {
            _logger = logger;

        }
    }
}
