using Jobcandidate.Application.Services;

namespace Jobcandidate.Application;

public interface IManualService
{
    List<PreferWayDto> GetPrefer();
}
