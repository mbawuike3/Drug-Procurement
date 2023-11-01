using Drug_Procurement.Context;
using Drug_Procurement.Models;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace Drug_Procurement.SeederDb
{
    public static class RoleSeeder
    {
        public static async Task<IServiceCollection> Initialize(this IServiceCollection services, IServiceProvider serviceProvider)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                dbContext.Database.EnsureCreated(); 
                await dbContext.SaveChangesAsync();
                //await dbContext.Database.MigrateAsync();
                var roles =await dbContext.Roles.ToListAsync();
                if(!roles.Any()) 
                {
                    var now = DateTime.Now;
                    //Admin, Supplier, HealthCare Provider
                    roles = new List<Roles>
                    {
                        new Roles{ Name = "Admin", DateCreated = now, Description = "This user has all the permissions" },
                        new Roles{ Name = "Supplier", DateCreated = now, Description = "This user has the permission to create an Inventory" },
                        new Roles{ Name = "HealthCare Provider", DateCreated = now, Description = "This user has the permission to View and Get an Inventory" }
                        
                    };
                    await dbContext.Roles.AddRangeAsync(roles);
                    await dbContext.SaveChangesAsync();
                }
            }
            return services;
        }
    }
}
