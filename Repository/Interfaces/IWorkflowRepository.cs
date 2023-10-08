using CapitalPlacementAssessment.Domain;
using CapitalPlacementAssessment.Domain.DTOs;

namespace CapitalPlacementAssessment.Repository.Implementations
{
    public interface IWorkflowRepository
    {
        Task<ResponseClass<WorkflowDto>> CreateWorkFlow(WorkflowDto request);
        Task<ResponseClass<WorkflowDto>> GetWorkFlow(string programId);
        Task<ResponseClass<WorkflowDto>> UpdateWorkFlow(WorkflowDto request);
    }
}