using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LeaveManager.Data
{
    [Table("LeaveTypes")]
    public class LeaveType
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public int DefaultDays { get; set; }
        public string Description { get; set; }
        public DateTime DateCreated { get; set; }
    }
}