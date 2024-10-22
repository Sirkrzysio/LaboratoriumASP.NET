using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace WebAPP.Models;

public class ContactModel
{
    [HiddenInput]
    public int Id { get; set; }
    [Required]
    [MaxLength(length:20, ErrorMessage = "Imię nie może być dłuższe od 20 znaków")]
    [MinLength(length:2, ErrorMessage = "Imie nie moze byc krotsze niz 2")]
    public string FirstName { get; set; }
    [Required]
    [MaxLength(length:30, ErrorMessage = "Nazwisko nie może być dłuższe od 30 znaków")]
    [MinLength(length:2, ErrorMessage = "Nazwisko nie moze byc krotsze niz 2")]
    public string LastName { get; set; }
    [EmailAddress]
    public string Email { get; set; }
    [Phone]
    [RegularExpression("\\d{3} \\d{3} \\d{3}", ErrorMessage = "Wpisz numer wg wzoru xxx xxx xxx")]
    public string PhoneNumber { get; set; }
    [DataType(DataType.Date)]
    public DateOnly BirthDate { get; set; }
    
}