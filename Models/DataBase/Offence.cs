using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DepartmentRating.DataBase
{
    public class Offence
    {
        [Key]
        public int OffenceId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [RegularExpression("([1-9][0-9]*)", ErrorMessage = "Count must be a natural number")]
        public int Cost { get; set; }
        public string Owner { get; set; }
        public DateTime CreateDate { get; set; }

        public virtual ICollection<Main> Main { get; set; }

        public override string ToString()
        {
            return "Event";
        }
    }
}
