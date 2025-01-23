using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication2.Entities;

public partial class University
{
    public int Id { get; set; }

    public int? CountryId { get; set; }

    public string? UniversityName { get; set; }

    public virtual Country? Country { get; set; }
}
