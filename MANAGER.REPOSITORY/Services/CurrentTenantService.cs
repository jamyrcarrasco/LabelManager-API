using MANAGER.DAL;
using MANAGER.REPOSITORY.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MANAGER.REPOSITORY.Services
{
    public class CurrentTenantService : ICurrentTenant
    {
        private readonly AppDbContext _appDbContext;
        public CurrentTenantService(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public string? TenantId { get; set; }

        public async Task<bool> SetTenant(string tenant)
        {
            var tenantInfo = await _appDbContext.Tenant.Where(x => x.Id == tenant).FirstOrDefaultAsync();

            if (tenantInfo != null)
            {
                TenantId = tenantInfo.Id;
                return true;
            }
            throw new Exception("Tenant Invalid");
        }
    }
}
