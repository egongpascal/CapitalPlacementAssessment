using AutoMapper;
using CapitalPlacementAssessment.Domain.DTOs;
using CapitalPlacementAssessment.Models;

namespace CapitalPlacementAssessment.Repository
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<ProgramDetails, ProgramDetailsDto>().ReverseMap();
        }
    }
}
