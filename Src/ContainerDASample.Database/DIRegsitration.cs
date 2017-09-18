
using ContainerDASample.Database.Repository;

using Microsoft.Extensions.DependencyInjection;
using System;

namespace ContainerDASample.Database
{
    public static class DIRegistration
    {
        public static void RegisterRepository(IServiceCollection services)
        {
            
            services.AddScoped<IEmpRepository, EmployeeRepository>();
        }
    }
}
