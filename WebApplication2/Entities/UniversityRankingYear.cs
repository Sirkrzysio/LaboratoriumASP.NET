using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication2.Entities;

public partial class UniversityRankingYear
{
    public int? UniversityId { get; set; }

    public int? RankingCriteriaId { get; set; }

    [Range(2016, 2025, ErrorMessage = "Year must be between 2016 and 2025")]
    public int? Year { get; set; }

    [Range(0, 100, ErrorMessage = "Score must be between 0 and 100")]
    public int? Score { get; set; }

    public virtual RankingCriterion? RankingCriteria { get; set; }

    public virtual University? University { get; set; }
    
    [NotMapped]
    public int RankingSystemId { get; set; }
    
    [NotMapped]
    public virtual RankingSystem RankingSystem { get; set; }
}
