using CapitalPlacementAssessment.Models;

namespace CapitalPlacementAssessment.Repository.Implementations
{
    public interface IProgramRepository
    {
        Task<ProgramDetails> CreateProgram(ProgramDetails program);
        Task<ProgramDetails> GetProgram(int id);
        Task<ProgramDetails> UpdateProgram(ProgramDetails program);
    }
}