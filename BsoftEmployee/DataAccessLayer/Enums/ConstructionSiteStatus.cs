using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.Enums;

public enum ConstructionSiteStatus
{
    [Display(Name = "Aangemaakt")]
    Created = 0,
    [Display(Name = "Goedgekeurd")]
    Approved = 1,
    [Display(Name = "In werking")]
    InProgress = 2,
    [Display(Name = "Afgerond")]
    Completed = 3
}
