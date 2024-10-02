namespace Jobcandidate.Domain;

using System.ComponentModel.DataAnnotations;

public class Candidate : Auditable
{

    [Required]
    [MaxLength(50)]
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

}
