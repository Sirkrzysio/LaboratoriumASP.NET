using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc;
using WebAPP.Models;

namespace WebApp.Models.Services;

[Table("contacts")]
public class ContactEntity {
    
    [HiddenInput] public int Id { get; set; }

    [Required]
    [MaxLength(20)]
    [MinLength(2)]
    public string FirstName { get; set; }

    [Required]
    [MaxLength(50)]
    [MinLength(2)]
    public string LastName { get; set; }

    public string Email { get; set; }
    
    public string PhoneNumber { get; set; }
    
    [Column("birth_date")]
    public DateOnly BirthDate { get; set; }
    
    public Category Category { get; set; }
    
    public DateTime Created { get; set; }
    
    public int OrganizationId { get; set; }
    
    public OrganizationEntity? Organization { get; set; }
}