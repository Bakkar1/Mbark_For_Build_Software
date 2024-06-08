using DataAccessLayer.Enums;
using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.DTOs;

public class CreateConstructionSiteDTO
{
    [Required]
    public string? Name { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public ConstructionSiteStatus Status { get; set; }
}

