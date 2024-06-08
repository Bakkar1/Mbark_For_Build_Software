using DataAccessLayer.Enums;

namespace DataAccessLayer.DTOs;

public class ConstructionSiteDTO
{
    public int ConstructionSiteId { get; set; }
    public string? Name { get; set; }
    public string? StartDate { get; set; }
    public string? EndDate { get; set; }
    public string? Status { get; set; }

    public List<EmployeeDTO> Employees { get; set; } = new List<EmployeeDTO>();
}
