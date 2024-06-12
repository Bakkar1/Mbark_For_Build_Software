using DataAccessLayer.Enums;

namespace DataAccessLayer.DTOs;
public class EmployeeDTO
{
    public string? EmployeeId { get; set; }
    public string? Name { get; set; }
    public string? Role { get; set; }
    public List<string>? Sites { get; set; }
}

