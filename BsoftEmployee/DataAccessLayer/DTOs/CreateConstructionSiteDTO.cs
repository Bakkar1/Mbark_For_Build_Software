using DataAccessLayer.Enums;

namespace DataAccessLayer.DTOs;

public class CreateConstructionSiteDTO
{
    public string? Name { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public ConstructionSiteStatus Status { get; set; }
}

