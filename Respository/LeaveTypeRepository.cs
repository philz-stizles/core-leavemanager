using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LeaveManager.Contracts;
using LeaveManager.Data;
using Microsoft.EntityFrameworkCore;

namespace LeaveManger.Repository
{
	public class LeaveTypeRepository : ILeaveTypeRepository
    {
        private readonly ApplicationDbContext _db;

        public LeaveTypeRepository(ApplicationDbContext db)
        {
            _db = db;
        } 

        public async Task<bool> CreateAsync(LeaveType entity)
        {
            await _db.LeaveTypes.AddAsync(entity);
            return await SaveAsync(); 
        }

        public async Task<bool> DeleteAsync(LeaveType entity)
        {

            _db.LeaveTypes.Remove(entity);
            return await SaveAsync();
        }

        public async Task<ICollection<LeaveType>> FindAllAsync()
        {
            return await _db.LeaveTypes.ToListAsync();
        }

        public async Task<LeaveType> FindByIdAsync(int id)
        {
            return await _db.LeaveTypes.FindAsync(id);
        }

        public ICollection<LeaveType> GetEmployeesByLeaveType(int id)
        {
            throw new NotImplementedException();
        }

        private async Task<bool> SaveAsync()
        {
            return await _db.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateAsync(LeaveType entity)
        {
            _db.LeaveTypes.Update(entity);
            return await SaveAsync();
        }

        public async Task<bool> IsExistsAsync(int id)
        {
            return await _db.LeaveTypes.AnyAsync(l => l.Id == id);
        }
    }
}