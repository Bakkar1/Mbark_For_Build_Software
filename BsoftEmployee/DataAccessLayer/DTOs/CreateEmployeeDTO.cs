using DataAccessLayer.Enums;

namespace DataAccessLayer.DTOs;

public class CreateEmployeeDTO
{
    public string? Name { get; set; }
    public EmployeeRole Role { get; set; }
}
