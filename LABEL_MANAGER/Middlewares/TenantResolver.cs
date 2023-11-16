using MANAGER.REPOSITORY.Interfaces;

namespace LABEL_MANAGER.Middlewares
{
    public class TenantResolver
    {
        private readonly RequestDelegate _next;

        public TenantResolver (RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext, ICurrentTenant currentTenant)
        {
            httpContext.Request.Headers.TryGetValue("Tenant-Identifier", out var tenanFromHeader);
            if (!string.IsNullOrEmpty(tenanFromHeader))
            {
                await currentTenant.SetTenant(tenanFromHeader);
            }

            await _next(httpContext);
        }
    }
}
