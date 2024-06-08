using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.Enums;

public enum EmployeeRole
{
    [Display(Name = "Metselaar")]
    Mason = 0,
    [Display(Name = "Schrijnwerker")]
    Carpenter = 1,
    [Display(Name = "Administratie")]
    Administration = 2,
    [Display(Name = "Manager")]
    Manager = 3
}
