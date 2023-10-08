using CapitalPlacementAssessment.Domain;
using CapitalPlacementAssessment.Domain.DTOs;

namespace CapitalPlacementAssessment.Repository.Implementations
{
    public interface IApplicationTemplateRepo
    {
        Task<ResponseClass<ApplicationTemplateDto>> UpdateApplicationTemplate(ApplicationTemplateDto request);
        Task<ResponseClass<ApplicationTemplateDto>> GetApplicationTemplate(string programId);
        Task<ResponseClass<ApplicationTemplateDto>> CreateApplicationTemplate(ApplicationTemplateDto request);
    }
}