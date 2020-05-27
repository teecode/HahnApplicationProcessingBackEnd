using Hahn.ApplicatonProcess.May2020.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Hahn.ApplicatonProcess.May2020.Data
{
    public class HahnDataContext : DbContext
    {
        public HahnDataContext(DbContextOptions<HahnDataContext> options)
            : base(options)
        {
        }

        public DbSet<Applicant> Applicants { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Applicant>(
                b =>
                {
                    b.HasKey(x => x.ID);
                });
        }
    }
}