using Jobcandidate.Application.Services;
using Jobcandidate.Domain;

namespace Jobcandidate.Application;

public class ManualService : IManualService
{
    public List<PreferWayDto> GetPrefer()
    {
        return Enum.GetValues(typeof(PreferWay))
                   .Cast<PreferWay>()
                   .Select(p => new PreferWayDto
                   {
                       Id = (int)p,
                       Name = p.ToString()
                   })
                   .ToList();
    }

}
