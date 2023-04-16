using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Onion.JwtApp.Application.Interfaces;
using Onion.JwtApp.Persistance.Context;
using Onion.JwtApp.Persistance.Repositories;

namespace Onion.JwtApp.Persistance
{
    public static class ServiceRegistration
    {
        public static void AddPersistanceServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<JwtContext>(opt =>
            {
                opt.UseSqlServer(configuration.GetConnectionString("Express"));
            });
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        }
    }
}
