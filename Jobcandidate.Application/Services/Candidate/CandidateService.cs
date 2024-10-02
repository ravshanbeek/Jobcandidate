
using AutoMapper;
using Jobcandidate.Domain;
using Jobcandidate.Shared;

namespace Jobcandidate.Application;

public class CandidateService : ICandidateService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CandidateService(IUnitOfWork unitOfWork,
        IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<CandiateDto> CreateOrModify(CandiateCreateOrModifyDto dto)
    {
        var transaction = await _unitOfWork.BeginTransactionAsync();
        try
        {
            var maybeUser = _unitOfWork.CandidateRepository
            .FindByCondition(x => x.Email == dto.Email)
            .FirstOrDefault();

            if (maybeUser is not null)
            {
                maybeUser.FirstName = dto.FirstName;
                maybeUser.LastName = dto.LastName;
                maybeUser.PhoneNumber = dto.PhoneNumber;
                maybeUser.LinkedInProfile = dto.LinkedInProfile;
                maybeUser.GitHubProfile = dto.GitHubProfile;
                maybeUser.Comments = dto.Comments;
                maybeUser.PreferWay = (PreferWay)dto.PreferWay;

                await _unitOfWork.CandidateRepository.Update(maybeUser);

                await _unitOfWork.SaveChangesAsync();

                transaction.Commit();

                return _mapper.Map<CandiateDto>(dto);
            }
            else
            {
                Candidate candidate = new()
                {
                    FirstName = dto.FirstName,
                    LastName = dto.LastName,
                    PhoneNumber = dto.PhoneNumber,
                    Email = dto.Email,
                    LinkedInProfile = dto.LinkedInProfile,
                    GitHubProfile = dto.GitHubProfile,
                    Comments = dto.Comments,
                    PreferWay = (PreferWay)dto.PreferWay,
                    CallTimeInterval = dto.CallTimeInterval,
                };

                var createdCandidate = await _unitOfWork.CandidateRepository.Create(candidate);
                await _unitOfWork.SaveChangesAsync();
                transaction.Commit();

                return _mapper.Map<CandiateDto>(createdCandidate);
            }
        }
        catch (Exception ex)
        {
            _unitOfWork.Rollback();
            throw;
        }
    }
}
