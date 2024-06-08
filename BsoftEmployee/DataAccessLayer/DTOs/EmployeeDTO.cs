using DataAccessLayer.Enums;

namespace DataAccessLayer.DTOs;

public class EmployeeDTO
{
    public int EmployeeId { get; set; }
    public string? Name { get; set; }
    public EmployeeRole Role { get; set; }
}

