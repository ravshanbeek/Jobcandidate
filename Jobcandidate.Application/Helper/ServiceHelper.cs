using Jobcandidate.Domain;

namespace Jobcandidate.Application;

public static class ServiceHelper
{
    public static T CheckNull<T>(this T obj, string message = null)
    {
        if (obj is null)
        {
            var type = typeof(T);
            throw new CandidateException(404, message ?? $"{type.Name} not found!");
        }

        return obj;
    }
}
