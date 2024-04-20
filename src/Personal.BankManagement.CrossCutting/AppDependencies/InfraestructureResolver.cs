using Microsoft.Extensions.Configuration; 
using Microsoft.Extensions.DependencyInjection;

namespace Personal.BankManagement.CrossCutting;

public static class InfraestructureResolver
{
    public static IServiceCollection AddInfraestructure(this IServiceCollection services, IConfiguration configuration)
    {
        //services.AddDbContext<BankManagementContext>(options =>
        // options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
        return services;
    }

}
