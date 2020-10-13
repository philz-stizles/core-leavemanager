using AutoMapper;
using LeaveManager.Data;
using LeaveManager.Models;
using Microsoft.AspNetCore.Identity;

namespace LeaveManger.Mappings
{
	public class LeaveProfile : Profile
    {
        public LeaveProfile(){
            CreateMap<LeaveType, LeaveTypeVM>().ReverseMap();
            CreateMap<LeaveType, LeaveTypeCreateVM>().ReverseMap();
            CreateMap<Employee, EmployeeVM>().ReverseMap();
        }
    }
}