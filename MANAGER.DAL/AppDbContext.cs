using MANAGER.MODELS;
using MANAGER.REPOSITORY.Interfaces;
using MANAGER.REPOSITORY.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MANAGER.DAL
{
    public class AppDbContext : DbContext
    {
        private readonly ICurrentTenant _currentTenant;
        public string CurrentTenantId { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options, ICurrentTenant currentTenant) : base(options) 
        {
            _currentTenant = currentTenant;
            CurrentTenantId = currentTenant.TenantId;
        }

        public DbSet<Products> Products { get; set; }
        //public DbSet<Tenant> Tenant { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Products>().HasQueryFilter(x => x.TenantId == CurrentTenantId);
        }

        public override int SaveChanges()
        {
            foreach (var entry in ChangeTracker.Entries<IMustHaveTenant>().ToList())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                    case EntityState.Modified:
                        entry.Entity.TenantId = CurrentTenantId;
                            break;
                }
            }
            var result = base.SaveChanges();
            return result;
        }

    }
}
