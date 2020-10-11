using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LeaveManager.Models
{
    [Table("LeaveHistories")]
    public class LeaveHistory
    {
        [Key]
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime DateRequested { get; set; }
        public DateTime DateActioned { get; set; }
        public bool? Approved { get; set; }

        [ForeignKey("RequestingEmployeeId")]
        public Employee RequestingEmployee { get; set; }
        public string RequestingEmployeeId { get; set; }

        [ForeignKey("LeaveTypeId")]
        public LeaveType LeaveType { get; set; }
        public int LeaveTypeId { get; set; }

        [ForeignKey("ApprovedById")]
        public Employee ApprovedBy { get; set; }
        public string ApprovedById { get; set; }
    }
}