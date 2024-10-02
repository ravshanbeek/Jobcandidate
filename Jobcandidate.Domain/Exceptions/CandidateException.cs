namespace Jobcandidate.Domain;

public class CandidateException : Exception
{
    public int StatusCode { get; set; }
    public CandidateException(int statusCode, string message) : base(message)
    {
        this.StatusCode = statusCode;
    }
}
