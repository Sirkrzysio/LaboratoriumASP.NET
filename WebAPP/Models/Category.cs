using System.ComponentModel.DataAnnotations;

namespace WebApp.Models;

public enum Category
{
    [Display(Name = "Rodzina")]
    Family,
    [Display(Name = "Znajomi")]
    Friend,
    [Display(Name = "Kontakty zawodowe")]
    Business,
}