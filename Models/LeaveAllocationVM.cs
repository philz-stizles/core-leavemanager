using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LeaveManager.Models
{
    public class LeaveAllocationCreateVM
    {
        public int NumberOfDays { get; set; }
        public DateTime DateCreated { get; set; }
        public int Period { get; set; }
        public string EmployeeId { get; set; }
        public int LeaveTypeId { get; set; }
    }

    public class LeaveAllocationVM
    {
        public ICollection<LeaveTypeVM> LeaveTypes { get; set; }
        public int NumberUpdated { get; set; }
    }

    public class LeaveAllocationeVM
    {
        public int Id { get; set; }
        public int NumberOfDays { get; set; }
        public DateTime DateCreated { get; set; }
        public EmployeeVM Employee { get; set; }
        public string EmployeeId { get; set; }
        public LeaveTypeVM LeaveType { get; set; }
        public int LeaveTypeId { get; set; }
        public IEnumerable<SelectListItem> Employees { get; set; }
    }
}