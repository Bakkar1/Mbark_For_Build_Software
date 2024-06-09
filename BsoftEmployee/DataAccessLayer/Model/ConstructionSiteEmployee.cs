using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayer.Model;

public class ConstructionSiteEmployee
{
    [Key]
    public int ConstructionSiteEmployeeId { get; set; }
    [Required]
    public int ConstructionSiteId { get; set; }
    [ForeignKey(nameof(ConstructionSiteId))]
    public ConstructionSite? ConstructionSite { get; set; }
    [Required]
    public string? EmployeeId { get; set; }
    [ForeignKey(nameof(EmployeeId))]
    public Employee? Employee { get; set; }
}
