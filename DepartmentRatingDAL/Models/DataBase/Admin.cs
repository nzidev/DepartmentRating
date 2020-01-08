using System;
using System.ComponentModel.DataAnnotations;

namespace DepartmentRatingDAL.Models.DataBase
{
    public class Admin
    {
        [Key]
        public int AdminId { get; set; }

        [Required]
        [Display(Name="Имя администратора")]
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public string Owner { get; set; }

        public override string ToString()
        {
            return "Admin";
        }
    }
}
