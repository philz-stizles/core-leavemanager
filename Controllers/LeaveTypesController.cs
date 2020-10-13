using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using LeaveManager.Models;
using AutoMapper;
using LeaveManager.Contracts;
using System.Collections.Generic;
using System.Threading.Tasks;
using LeaveManager.Data;
using System;

namespace LeaveManager.Controllers
{
    public class LeaveTypesController : Controller
    {
        private readonly ILogger<LeaveTypesController> _logger;
        private readonly IMapper _mapper;
        private readonly ILeaveTypeRepository _leaveTypeRepo;

        public LeaveTypesController(ILogger<LeaveTypesController> logger, IMapper mapper, 
            ILeaveTypeRepository leaveTypeRepo)
        {
            _mapper = mapper;
            _leaveTypeRepo = leaveTypeRepo;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            var leaveTypes = await _leaveTypeRepo.FindAllAsync();
            var model = _mapper.Map<List<LeaveTypeVM>>(leaveTypes);
            return View(model);
        }

        public async Task<IActionResult> Details(int id)
        {
            var isExists = await _leaveTypeRepo.IsExistsAsync(id);
            if(!isExists){
                return NotFound();
            }

            var leaveType = await _leaveTypeRepo.FindByIdAsync(id);
            var model = _mapper.Map<LeaveTypeVM>(leaveType);
            return View(model);
        }
        
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(LeaveTypeCreateVM model)
        {
            try
            {
                if(!ModelState.IsValid){
                    return View(model);
                }

                var leaveType = _mapper.Map<LeaveType>(model);
                leaveType.DateCreated = DateTime.Now;
                var isSuccess = await _leaveTypeRepo.CreateAsync(leaveType);
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
            var isExists = await _leaveTypeRepo.IsExistsAsync(id);
            if(!isExists){
                return NotFound();
            }

            var leaveType = await _leaveTypeRepo.FindByIdAsync(id);
            var model = _mapper.Map<LeaveTypeCreateVM>(leaveType);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(LeaveTypeCreateVM model)
        {
            try
            {
                if(!ModelState.IsValid){
                    return View(model);
                }

                var leaveType = _mapper.Map<LeaveType>(model);
                // leaveType.DateCreated = DateTime.Now;
                var isSuccess = await _leaveTypeRepo.UpdateAsync(leaveType);
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
            var isExists = await _leaveTypeRepo.IsExistsAsync(id);
            if(!isExists){
                return NotFound();
            }

            var leaveType = await _leaveTypeRepo.FindByIdAsync(id);
            var model = _mapper.Map<LeaveTypeVM>(leaveType);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(LeaveTypeVM model)
        {
            try
            {
                if(!ModelState.IsValid){
                    return View(model);
                }

                var leaveType = _mapper.Map<LeaveType>(model);
                var isSuccess = await _leaveTypeRepo.DeleteAsync(leaveType);
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
