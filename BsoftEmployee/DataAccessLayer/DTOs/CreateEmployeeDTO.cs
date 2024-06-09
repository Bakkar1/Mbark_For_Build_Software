using DataAccessLayer.Enums;
using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.DTOs;

public class CreateEmployeeDTO
{
    [Required]
    public string? FirstName { get; set; }
    [Required, EmailAddress]
    public string? Email { get; set; }
    [Required]
    [DataType(DataType.Password)]
    public string? Password { get; set; }
    public EmployeeRole Role { get; set; }
}
