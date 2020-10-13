using System;
using System.ComponentModel.DataAnnotations;

namespace LeaveManager.Models
{
    public class EmployeeVM
    {
        public int Id { get; set; }
        [Display(Name ="User Name")]
        public string UserName { get; set; }
        public string Email { get; set; }
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string TaxId { get; set; }
        [Display(Name = "Date of Birth")]
        public DateTime DateOfBirth { get; set; }
        [Display(Name = "Date Joined")]
        public DateTime DateJoined { get; set; }
    }
}