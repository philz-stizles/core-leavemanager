using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LeaveManager.Contracts;
using LeaveManager.Data;
using Microsoft.EntityFrameworkCore;

namespace LeaveManger.Repository
{
    public class LeaveHistoryRepository : ILeaveHistoryRepository
    {
        private readonly ApplicationDbContext _db;

        public LeaveHistoryRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<bool> CreateAsync(LeaveHistory entity)
        {
            await _db.LeaveHistories.AddAsync(entity);
            return await SaveAsync(); 
        }

        public async Task<bool> DeleteAsync(LeaveHistory entity)
        {

            _db.LeaveHistories.Remove(entity);
            return await SaveAsync();
        }

        public async Task<ICollection<LeaveHistory>> FindAllAsync()
        {
            return await _db.LeaveHistories.ToListAsync();
        }

        public async Task<LeaveHistory> FindByIdAsync(int id)
        {
            return await _db.LeaveHistories.FindAsync(id);
        }

        private async Task<bool> SaveAsync()
        {
            return await _db.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateAsync(LeaveHistory entity)
        {
            _db.LeaveHistories.Update(entity);
            return await SaveAsync();
        }
        
        public async Task<bool> IsExistsAsync(int id)
        {
            return await _db.LeaveHistories.AnyAsync(l => l.Id == id);
        }
    }
}