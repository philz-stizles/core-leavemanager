using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LeaveManager.Contracts;
using LeaveManager.Data;
using Microsoft.EntityFrameworkCore;

namespace LeaveManger.Repository
{
	public class LeaveAllocationRepository : ILeaveAllocationRepository
    {
        private readonly ApplicationDbContext _db;

        public LeaveAllocationRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<bool> CreateAsync(LeaveAllocation entity)
        {
            await _db.LeaveAllocations.AddAsync(entity);
            return await SaveAsync(); 
        }

        public async Task<bool> DeleteAsync(LeaveAllocation entity)
        {

            _db.LeaveAllocations.Remove(entity);
            return await SaveAsync();
        }

        public async Task<ICollection<LeaveAllocation>> FindAllAsync()
        {
            return await _db.LeaveAllocations.ToListAsync();
        }

        public async Task<LeaveAllocation> FindByIdAsync(int id)
        {
            return await _db.LeaveAllocations.FindAsync(id);
        }

        public ICollection<LeaveAllocation> GetEmployeesByLeaveAllocation(int id)
        {
            throw new NotImplementedException();
        }

        private async Task<bool> SaveAsync()
        {
            return await _db.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateAsync(LeaveAllocation entity)
        {
            _db.LeaveAllocations.Update(entity);
            return await SaveAsync();
        }

        public async Task<bool> IsExistsAsync(int id)
        {
            return await _db.LeaveAllocations.AnyAsync(l => l.Id == id);
        }

        public async Task<bool> UserHasLeaveForPeriodAsync(int leaveTypeId, string employeeId)
        {
            var leaveAllocations = await FindAllAsync();
            return leaveAllocations
                .Where(la => la.LeaveTypeId == leaveTypeId
                    && la.EmployeeId == employeeId
                    && la.Period == DateTime.Now.Year
                ).Any();
        }
    }
}