using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.DTOs;
public class AddEmployeesToSiteDTO
{
    [Required]
    public int ConstructionSiteId { get; set; }
    [Required]
    public List<string>? EmployeeIds { get; set; }
}