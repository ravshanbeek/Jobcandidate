using AutoMapper;
using Jobcandidate.Domain;
using Jobcandidate.Shared;

namespace Jobcandidate.Application
{
    public class CandidateService : ICandidateService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CandidateService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<CandiateDto> CreateOrModify(CandiateCreateOrModifyDto dto)
        {
                var maybeUser = _unitOfWork.CandidateRepository
                    .FindByCondition(x => x.Email == dto.Email)
                    .FirstOrDefault();

                if (maybeUser is not null)
                {
                    // Update existing candidate
                    maybeUser.FirstName = dto.FirstName;
                    maybeUser.LastName = dto.LastName;
                    maybeUser.PhoneNumber = dto.PhoneNumber;
                    maybeUser.LinkedInProfile = dto.LinkedInProfile;
                    maybeUser.GitHubProfile = dto.GitHubProfile;
                    maybeUser.Comments = dto.Comments;
                    maybeUser.PreferWay = (PreferWay)dto.PreferWay;
                    maybeUser.CallTimeInterval = dto.CallTimeInterval;

                    await _unitOfWork.CandidateRepository.Update(maybeUser);

                    // Map and return the updated entity
                    var updatedCandidateDto = _mapper.Map<CandiateDto>(maybeUser);
                    await _unitOfWork.SaveChangesAsync();
                    return updatedCandidateDto;
                }
                else
                {
                    // Create a new candidate
                    var candidate = new Candidate
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

                    // Map and return the newly created entity
                    var createdCandidateDto = _mapper.Map<CandiateDto>(createdCandidate);
                    await _unitOfWork.SaveChangesAsync();
                    return createdCandidateDto;
                }
        }
    }
}
