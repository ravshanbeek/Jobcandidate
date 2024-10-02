using System.ComponentModel.DataAnnotations;

namespace Jobcandidate.Application;

public class CandiateCreateOrModifyDto
{
    [Required]
    public string FirstName { get; set; }

    [Required]
    [MaxLength(50)]
    public string LastName { get; set; }

    [Phone]
    public string PhoneNumber { get; set; }

    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [MaxLength(100)]
    public string LinkedInProfile { get; set; }

    [MaxLength(100)]
    public string GitHubProfile { get; set; }

    [Required]
    public string Comments { get; set; }
    public int? PreferWay { get; set; }
    public string? CallTimeInterval { get; set; }

}
