using DataAccessLayer.Enums;
using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.DTOs;

public class ConstructionSiteDTO
{
    public int ConstructionSiteId { get; set; }
    [Required]
    public string? Name { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public ConstructionSiteStatus Status { get; set; }
    public List<EmployeeDTO>? Employees { get; set; }
}
