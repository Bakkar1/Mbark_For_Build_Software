using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.Model;

public class Employee : IdentityUser
{
    [Required]
    public string? FirstName { get; set; }

    public ICollection<ConstructionSiteEmployee>? ConstructionSiteEmployees { get; set; }
}
