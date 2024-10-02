namespace Jobcandidate.Application;

public interface ICandidateService
{
    Task<CandiateDto> CreateOrModify(CandiateCreateOrModifyDto dto);
}
