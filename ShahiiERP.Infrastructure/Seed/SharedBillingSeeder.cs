using Microsoft.EntityFrameworkCore;
using ShahiiERP.Domain.Entities.Billing;
using ShahiiERP.Infrastructure.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShahiiERP.Infrastructure.Seed;

public static class SharedBillingSeeder
{
    public static async Task SeedAsync(SharedDbContext db)
    {
        // --- Seed Subscription Plans ---
        if (!await db.SubscriptionPlans.AnyAsync())
        {
            var free = new SubscriptionPlan
            {
                Id = Guid.NewGuid(),
                Name = "Free",
                PricePerStudent = 0m,
                BillingCycle = "None",
                IsFreeTier = true,
                StudentLimit = 150,
                CreatedAt = DateTime.UtcNow,
                IsDeleted = false
            };

            var pro = new SubscriptionPlan
            {
                Id = Guid.NewGuid(),
                Name = "Pro",
                PricePerStudent = 150m, // PKR per student
                BillingCycle = "Monthly",
                IsFreeTier = false,
                StudentLimit = null,
                CreatedAt = DateTime.UtcNow,
                IsDeleted = false
            };

            var enterprise = new SubscriptionPlan
            {
                Id = Guid.NewGuid(),
                Name = "Enterprise",
                PricePerStudent = 0m, // custom pricing
                BillingCycle = "Annual",
                IsFreeTier = false,
                StudentLimit = null,
                CreatedAt = DateTime.UtcNow,
                IsDeleted = false
            };

            db.SubscriptionPlans.AddRange(free, pro, enterprise);
            await db.SaveChangesAsync();

            // --- Seed Modules ---
            if (!await db.Modules.AnyAsync())
            {
                var modules = new[]
                {
                    new Module { Id=Guid.NewGuid(), Key="Admissions", Name="Admissions", IsCore=true, CreatedAt=DateTime.UtcNow },
                    new Module { Id=Guid.NewGuid(), Key="Students", Name="Student Management", IsCore=true, CreatedAt=DateTime.UtcNow },
                    new Module { Id=Guid.NewGuid(), Key="Attendance", Name="Attendance", IsCore=true, CreatedAt=DateTime.UtcNow },
                    new Module { Id=Guid.NewGuid(), Key="Communication", Name="Communication", CreatedAt=DateTime.UtcNow },
                    new Module { Id=Guid.NewGuid(), Key="Exams", Name="Examination", CreatedAt=DateTime.UtcNow },
                    new Module { Id=Guid.NewGuid(), Key="Academics", Name="Academics", CreatedAt=DateTime.UtcNow },
                    new Module { Id=Guid.NewGuid(), Key="Timetable", Name="Timetable", CreatedAt=DateTime.UtcNow },
                    new Module { Id=Guid.NewGuid(), Key="Fees", Name="Fees", CreatedAt=DateTime.UtcNow },
                    new Module { Id=Guid.NewGuid(), Key="Accounting", Name="Accounting", CreatedAt=DateTime.UtcNow },
                    new Module { Id=Guid.NewGuid(), Key="HR", Name="HR Management", CreatedAt=DateTime.UtcNow },
                    new Module { Id=Guid.NewGuid(), Key="Payroll", Name="Payroll", CreatedAt=DateTime.UtcNow },
                    new Module { Id=Guid.NewGuid(), Key="Reporting", Name="Reporting", CreatedAt=DateTime.UtcNow },
                    new Module { Id=Guid.NewGuid(), Key="Library", Name="Library", CreatedAt=DateTime.UtcNow },
                    new Module { Id=Guid.NewGuid(), Key="Transport", Name="Transport", CreatedAt=DateTime.UtcNow },
                    new Module { Id=Guid.NewGuid(), Key="Hostel", Name="Hostel", CreatedAt=DateTime.UtcNow },
                    new Module { Id=Guid.NewGuid(), Key="Inventory", Name="Inventory", CreatedAt=DateTime.UtcNow },
                    new Module { Id=Guid.NewGuid(), Key="MultiCampus", Name="Multi-Campus", CreatedAt=DateTime.UtcNow }
                };

                db.Modules.AddRange(modules);
                await db.SaveChangesAsync();

                // --- SubscriptionPlanModules mapping ---
                var planModules = new List<SubscriptionPlanModule>
                {
                    // Free Tier
                    new SubscriptionPlanModule { SubscriptionPlanId = free.Id, ModuleId = modules.First(m=>m.Key=="Admissions").Id },
                    new SubscriptionPlanModule { SubscriptionPlanId = free.Id, ModuleId = modules.First(m=>m.Key=="Students").Id },
                    new SubscriptionPlanModule { SubscriptionPlanId = free.Id, ModuleId = modules.First(m=>m.Key=="Attendance").Id },
                    new SubscriptionPlanModule { SubscriptionPlanId = free.Id, ModuleId = modules.First(m=>m.Key=="Communication").Id },

                    // Pro Tier
                    new SubscriptionPlanModule { SubscriptionPlanId = pro.Id, ModuleId = modules.First(m=>m.Key=="Exams").Id },
                    new SubscriptionPlanModule { SubscriptionPlanId = pro.Id, ModuleId = modules.First(m=>m.Key=="Academics").Id },
                    new SubscriptionPlanModule { SubscriptionPlanId = pro.Id, ModuleId = modules.First(m=>m.Key=="Timetable").Id },
                    new SubscriptionPlanModule { SubscriptionPlanId = pro.Id, ModuleId = modules.First(m=>m.Key=="Fees").Id },
                    new SubscriptionPlanModule { SubscriptionPlanId = pro.Id, ModuleId = modules.First(m=>m.Key=="Accounting").Id },
                    new SubscriptionPlanModule { SubscriptionPlanId = pro.Id, ModuleId = modules.First(m=>m.Key=="HR").Id },
                    new SubscriptionPlanModule { SubscriptionPlanId = pro.Id, ModuleId = modules.First(m=>m.Key=="Reporting").Id },

                    // Enterprise Tier
                    new SubscriptionPlanModule { SubscriptionPlanId = enterprise.Id, ModuleId = modules.First(m=>m.Key=="Library").Id },
                    new SubscriptionPlanModule { SubscriptionPlanId = enterprise.Id, ModuleId = modules.First(m=>m.Key=="Transport").Id },
                    new SubscriptionPlanModule { SubscriptionPlanId = enterprise.Id, ModuleId = modules.First(m=>m.Key=="Hostel").Id },
                    new SubscriptionPlanModule { SubscriptionPlanId = enterprise.Id, ModuleId = modules.First(m=>m.Key=="Inventory").Id },
                    new SubscriptionPlanModule { SubscriptionPlanId = enterprise.Id, ModuleId = modules.First(m=>m.Key=="MultiCampus").Id }
                };

                db.SubscriptionPlanModules.AddRange(planModules);
                await db.SaveChangesAsync();
            }
        }
    }
}
