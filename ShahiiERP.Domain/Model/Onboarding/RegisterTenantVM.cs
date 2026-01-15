using System.ComponentModel.DataAnnotations;

namespace ShahiiERP.Domain.Models.Onboarding;

public class RegisterTenantVM
{
    [Required]
    public Guid PlanId { get; set; }

    [Required, Display(Name = "School Name")]
    public string SchoolName { get; set; } = default!;

    [Required, Display(Name = "Admin Full Name")]
    public string AdminName { get; set; } = default!;

    [Required, EmailAddress]
    public string Email { get; set; } = default!;

    [Required, MinLength(6)]
    public string Password { get; set; } = default!;

    [Phone]
    public string? Phone { get; set; }
}
