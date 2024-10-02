using AutoMapper;
using Jobcandidate.Domain;

namespace Jobcandidate.Application;

public class MapperProfiler : Profile
{
    public MapperProfiler()
    {
        CreateMap<Candidate, CandiateDto>().ReverseMap();
        CreateMap<Candidate, CandiateCreateOrModifyDto>().ReverseMap();
    }
}
