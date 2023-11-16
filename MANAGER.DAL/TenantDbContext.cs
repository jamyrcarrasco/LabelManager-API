using MANAGER.MODELS;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MANAGER.DAL
{
    public class TenantDbContext : DbContext
    {
        public TenantDbContext (DbContextOptions<TenantDbContext> options) : base(options)
        {

        }

        public DbSet<Tenant> Tenant { get; set; }   
    }
}
