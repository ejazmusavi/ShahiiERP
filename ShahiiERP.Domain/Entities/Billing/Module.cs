namespace ShahiiERP.Domain.Entities.Billing;

public class Module : BaseEntity
{
    public string Key { get; set; } // HR, Fees, Exams, etc.
    public string Name { get; set; }
    public string? Description { get; set; }
    public bool IsCore { get; set; } = false; // Free tier?
}