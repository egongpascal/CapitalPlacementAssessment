using CapitalPlacementAssessment.Domain;
using CapitalPlacementAssessment.Domain.DTOs;
using CapitalPlacementAssessment.Models;

namespace CapitalPlacementAssessment.Repository.Implementations
{
    public interface IProgramRepository
    {
        Task<ResponseClass<ProgramDetailsDto>> CreateProgram(ProgramDetailsDto program);
        Task<ResponseClass<ProgramDetailsDto>> GetProgram(string id);
        Task<ResponseClass<PreviewDto>> GetPreview(string programId);
        Task<ResponseClass<ProgramDetailsDto>> UpdateProgram(ProgramDetailsDto program);
    }
}