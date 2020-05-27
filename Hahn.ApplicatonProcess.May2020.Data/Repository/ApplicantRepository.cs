using Hahn.ApplicatonProcess.May2020.Data.IRepositories;
using Hahn.ApplicatonProcess.May2020.Data.Models;

namespace Hahn.ApplicatonProcess.May2020.Data.Repository
{
    public class ApplicantRepository : BaseRepository<Applicant>, IApplicantRepository
    {
        public ApplicantRepository(HahnDataContext dataEngineDbContext) : base(dataEngineDbContext)
        {
        }
    }
}