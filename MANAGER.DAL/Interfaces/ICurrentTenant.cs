using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MANAGER.REPOSITORY.Interfaces
{
    public interface ICurrentTenant
    {
        public string? TenantId { get; set; }
        public Task<bool> SetTenant(string tenant);
    }
}
