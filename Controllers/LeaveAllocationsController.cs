using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using LeaveManager.Models;
using AutoMapper;
using LeaveManager.Contracts;
using System.Collections.Generic;
using System.Threading.Tasks;
using LeaveManager.Data;
using System;
using Microsoft.AspNetCore.Identity;

namespace LeaveManager.Controllers
{
    public class LeaveAllocationsController : Controller
    {
        private readonly ILogger<LeaveAllocationsController> _logger;
        private readonly IMapper _mapper;
        private readonly ILeaveTypeRepository _leaveTypeRepo;
        private readonly ILeaveAllocationRepository _leaveAllocationRepo;
        private readonly UserManager<Employee> _userMgr;

        public LeaveAllocationsController(ILogger<LeaveAllocationsController> logger, IMapper mapper, 
            ILeaveTypeRepository leaveTypeRepo, ILeaveAllocationRepository leaveAllocationRepo, UserManager<Employee> userMgr)
        {
            _mapper = mapper;
            _leaveTypeRepo = leaveTypeRepo;
            _leaveAllocationRepo = leaveAllocationRepo;
            _logger = logger;
            _userMgr = userMgr;
        }

        public async Task<IActionResult> Index()
        {
            var leaveTypes = await _leaveTypeRepo.FindAllAsync();
            var leaveTypeVMs = _mapper.Map<List<LeaveTypeVM>>(leaveTypes);
            var model = new LeaveAllocationVM{
                LeaveTypes = leaveTypeVMs,
                NumberUpdated = 0
            };

            return View(model);
        }

        public async Task<IActionResult> SetLeave(int id)
        {
            var leaveType = await _leaveTypeRepo.FindByIdAsync(id);
            var employees = await _userMgr.GetUsersInRoleAsync("Employee");
            foreach(var employee in employees)
            {
                if(await _leaveAllocationRepo.UserHasLeaveForPeriodAsync(leaveType.Id, employee.Id))
                    continue;

                var allocation = new LeaveAllocationCreateVM {
                    DateCreated = DateTime.Now,
                    Period = DateTime.Now.Year,
                    EmployeeId = employee.Id,
                    LeaveTypeId = leaveType.Id,
                    NumberOfDays = leaveType.DefaultDays
                };

                var leaveAllocation = _mapper.Map<LeaveAllocation>(allocation);
                await _leaveAllocationRepo.CreateAsync(leaveAllocation);
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Employees()
        {
            var employees = await _userMgr.GetUsersInRoleAsync("Employee");
            var model = _mapper.Map<List<EmployeeVM>>(employees);
            return View(model);
        }

        public async Task<IActionResult> Details(int id)
        {
            var isExists = await _leaveAllocationRepo.IsExistsAsync(id);
            if(!isExists){
                return NotFound();
            }

            var leaveAllocation = await _leaveAllocationRepo.FindByIdAsync(id);
            var model = _mapper.Map<LeaveAllocationVM>(leaveAllocation);
            return View(model);
        }
        
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(LeaveAllocationCreateVM model)
        {
            try
            {
                if(!ModelState.IsValid){
                    return View(model);
                }

                var leaveAllocation = _mapper.Map<LeaveAllocation>(model);
                leaveAllocation.DateCreated = DateTime.Now;
                var isSuccess = await _leaveAllocationRepo.CreateAsync(leaveAllocation);
                if(!isSuccess){
                    ModelState.AddModelError("", "Something went wrong");
                    return View(model);
                }
                
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                
                ModelState.AddModelError("", "Something went wrong");
                return View(model);
            }
        }

        public async Task<IActionResult> Edit(int id)
        {
            var isExists = await _leaveAllocationRepo.IsExistsAsync(id);
            if(!isExists){
                return NotFound();
            }

            var leaveAllocation = await _leaveAllocationRepo.FindByIdAsync(id);
            var model = _mapper.Map<LeaveAllocationCreateVM>(leaveAllocation);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(LeaveAllocationCreateVM model)
        {
            try
            {
                if(!ModelState.IsValid){
                    return View(model);
                }

                var leaveAllocation = _mapper.Map<LeaveAllocation>(model);
                // leaveAllocation.DateCreated = DateTime.Now;
                var isSuccess = await _leaveAllocationRepo.UpdateAsync(leaveAllocation);
                if(!isSuccess){
                    ModelState.AddModelError("", "Something went wrong");
                    return View(model);
                }
                
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                
                ModelState.AddModelError("", "Something went wrong");
                return View(model);
            }
        }

        public async Task<IActionResult> Delete(int id)
        {
            var isExists = await _leaveAllocationRepo.IsExistsAsync(id);
            if(!isExists){
                return NotFound();
            }

            var leaveAllocation = await _leaveAllocationRepo.FindByIdAsync(id);
            var model = _mapper.Map<LeaveAllocationVM>(leaveAllocation);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(LeaveAllocationVM model)
        {
            try
            {
                if(!ModelState.IsValid){
                    return View(model);
                }

                var leaveAllocation = _mapper.Map<LeaveAllocation>(model);
                var isSuccess = await _leaveAllocationRepo.DeleteAsync(leaveAllocation);
                if(!isSuccess){
                    ModelState.AddModelError("", "Something went wrong");
                    return View(model);
                }
                
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                
                ModelState.AddModelError("", "Something went wrong");
                return View(model);
            }
        }
    }
}
