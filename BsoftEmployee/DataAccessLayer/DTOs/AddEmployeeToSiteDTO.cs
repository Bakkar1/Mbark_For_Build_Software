using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.DTOs;

public class AddEmployeeToSiteDTO
{
    [Required]
    public int ConstructionSiteId { get; set; }
    [Required]
    public string? EmployeeId { get; set; }
}

