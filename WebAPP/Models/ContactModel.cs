using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Models;

public class ContactModel
{
    [HiddenInput] public int Id { get; set; }

    [Required]
    [MaxLength(length:20, ErrorMessage = "Imie nie moze byc wieksze niz 20 znakow.")]
    [MinLength(length:2, ErrorMessage = "Imie nie moze byc mniejsze niz 2 znaki.")]
    [Display(Name = "Imie")]
    public string FirstName { get; set; }
    
    [Required]
    [MaxLength(length:50, ErrorMessage = "Imie nie moze byc wieksze niz 50 znakow.")]
    [MinLength(length:2, ErrorMessage = "Imie nie moze byc mniejsze niz 2 znaki.")]
    [Display(Name = "Nazwisko")]
    public string LastName { get; set; }
    [EmailAddress]
    [Display(Name = "Adres e-mail")]
    public string Email { get; set; }
    [Phone]
    [RegularExpression(pattern:"\\d{3} \\d{3} \\d{3}", ErrorMessage = "Wpisz numer wg wzoru: xxx xxx xxx")]
    [Display(Name = "Numer telefonu")]
    public string PhoneNumber { get; set; }
    [DataType(DataType.Date)]
    [Display(Name = "Data Urodzenia ")]
    public DateOnly BirthDate { get; set; }
    [Display(Name = "Kategoria")]
    public Category Category{ get; set; }
    
    
}