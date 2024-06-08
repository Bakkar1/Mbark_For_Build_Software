using DataAccessLayer.Enums;
using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.Model;

public class ConstructionSite
{
    public int ConstructionSiteId { get; set; }
    [Required]
    public string? Name { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public ConstructionSiteStatus Status { get; set; }

    public ICollection<ConstructionSiteEmployee>? ConstructionSiteEmployees { get; set; }
}
