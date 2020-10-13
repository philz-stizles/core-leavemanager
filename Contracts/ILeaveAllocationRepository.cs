using System.Threading.Tasks;
using LeaveManager.Data;

namespace LeaveManager.Contracts
{
    public interface ILeaveAllocationRepository : IRepositoryBase<LeaveAllocation>
    {
        Task<bool> UserHasLeaveForPeriodAsync(int leaveTypeId, string employeeId);
    }
}