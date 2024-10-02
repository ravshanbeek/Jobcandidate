using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Jobcandidate.Domain;

public abstract class Auditable
{
    [Key]
    public Guid Id { get; set; }
    public Guid? CreatedUserId { get; set; } = Guid.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
    public Guid? UpdatedUserId { get; set; }

}
