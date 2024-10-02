using System.ComponentModel.DataAnnotations;

namespace Jobcandidate.Application;

public class CandiateDto
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string? PhoneNumber { get; set; }
    public string Email { get; set; }
    public string? LinkedInProfile { get; set; }
    public string? GitHubProfile { get; set; }
    public string Comments { get; set; }
    public string? CallTimeInterval { get; set; }
}
