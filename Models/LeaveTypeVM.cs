using System;
using System.ComponentModel.DataAnnotations;

namespace LeaveManager.Models
{
    public class LeaveTypeCreateVM
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Display(Name="Default Number of Days")]
        [Range(1, 25, ErrorMessage = "Number of Days should be between 1 and 25")]
        public string DefaultDays { get; set; }
    }

    public class LeaveTypeVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        [Display(Name = "Default Number of Days")]
        public string DefaultDays { get; set; }
        [Display(Name="Date Created")]
        public DateTime DateCreated { get; set; }
    }
}