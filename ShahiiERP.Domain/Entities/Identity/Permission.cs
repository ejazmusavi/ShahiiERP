namespace ShahiiERP.Domain.Entities.Identity;

public class Permission : BaseEntity
{
    public string Key { get; set; } // e.g. Students.View
    public string Name { get; set; }
    public string Module { get; set; }
}
