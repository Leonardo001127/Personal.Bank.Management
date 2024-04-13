using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration; 
using Microsoft.Extensions.DependencyInjection;
using Personal.BankManagement.Infraestructure;
using Personal.BankManagement.Domain;

namespace Personal.BankManagement.CrossCutting;

public static class InfraestructureResolver
{
    public static IServiceCollection AddInfraestructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<BankManagementContext>(options =>
         options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
        services.AddScoped<IPersonRepository, PersonRepository>();
        return services;
    }

}
