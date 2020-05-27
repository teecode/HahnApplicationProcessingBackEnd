using Microsoft.EntityFrameworkCore.Storage;
using System.Threading.Tasks;

namespace Hahn.ApplicatonProcess.May2020.Data.IRepositories
{
    public interface IUnitofWork
    {
        IDbContextTransaction BeginTransaction();

        Task<bool> SubmitChangesAsync();
    }
}