using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ShahiiERP.Application.Common.Interfaces.Persistence;

namespace ShahiiERP.Infrastructure.Persistence.TenantDb;

public static class TenantDbSeeder
{
    /// <summary>
    /// Seed a new tenant database for first time use.
    /// For shared tenant mode, TenantId is required for partitioning.
    /// For dedicated tenant mode, TenantId is not used.
    /// </summary>
    public static async Task SeedAsync(ITenantDbContext tenantDb, Guid? tenantId = null)
    {
        // You can wrap in transaction if desired
        using var trx = await tenantDb.Database.BeginTransactionAsync();

        // 1. Academic Years / Sessions
        //await SeedAcademicYears(tenantDb, tenantId);

        //// 2. Fee Templates
        //await SeedFeeTemplates(tenantDb, tenantId);

        //// 3. Grade Scales
        //await SeedGradeScale(tenantDb, tenantId);

        //// 4. HR Foundations (Designations, Departments, etc)
        //await SeedHR(tenantDb, tenantId);

        //// 5. Timetable Defaults
        //await SeedTimetableDefaults(tenantDb, tenantId);

        //// 6. Holiday Calendar
        //await SeedHolidayCalendar(tenantDb, tenantId);

        await tenantDb.SaveChangesAsync();
        await trx.CommitAsync();
    }

    //private static async Task SeedAcademicYears(DbContext db, Guid? tenantId)
    //{
    //    var exists = await db.Set<AcademicYear>()
    //        .AnyAsync(x => tenantId == null || x.TenantId == tenantId);

    //    if (!exists)
    //    {
    //        db.Set<AcademicYear>().Add(new AcademicYear
    //        {
    //            Id = Guid.NewGuid(),
    //            TenantId = tenantId,
    //            Name = $"{DateTime.UtcNow.Year}-{DateTime.UtcNow.Year + 1}",
    //            StartsAt = new DateTime(DateTime.UtcNow.Year, 8, 1),
    //            EndsAt = new DateTime(DateTime.UtcNow.Year + 1, 7, 31),
    //            IsActive = true,
    //            CreatedAt = DateTime.UtcNow
    //        });
    //    }
    //}

    //private static async Task SeedFeeTemplates(DbContext db, Guid? tenantId)
    //{
    //    var exists = await db.Set<FeeTemplate>()
    //        .AnyAsync(x => tenantId == null || x.TenantId == tenantId);

    //    if (!exists)
    //    {
    //        db.Set<FeeTemplate>().Add(new FeeTemplate
    //        {
    //            Id = Guid.NewGuid(),
    //            TenantId = tenantId,
    //            Name = "Standard Fee Template",
    //            CreatedAt = DateTime.UtcNow,
    //            IsActive = true
    //        });
    //    }
    //}

    //private static async Task SeedGradeScale(DbContext db, Guid? tenantId)
    //{
    //    var exists = await db.Set<GradeScale>()
    //        .AnyAsync(x => tenantId == null || x.TenantId == tenantId);

    //    if (!exists)
    //    {
    //        db.Set<GradeScale>().AddRange(
    //            new GradeScale { Id = Guid.NewGuid(), TenantId = tenantId, Grade = "A", Min = 80, Max = 100 },
    //            new GradeScale { Id = Guid.NewGuid(), TenantId = tenantId, Grade = "B", Min = 70, Max = 79 },
    //            new GradeScale { Id = Guid.NewGuid(), TenantId = tenantId, Grade = "C", Min = 60, Max = 69 },
    //            new GradeScale { Id = Guid.NewGuid(), TenantId = tenantId, Grade = "D", Min = 50, Max = 59 },
    //            new GradeScale { Id = Guid.NewGuid(), TenantId = tenantId, Grade = "F", Min = 0, Max = 49 }
    //        );
    //    }
    //}

    //private static async Task SeedHR(DbContext db, Guid? tenantId)
    //{
    //    var exists = await db.Set<Designation>()
    //        .AnyAsync(x => tenantId == null || x.TenantId == tenantId);

    //    if (!exists)
    //    {
    //        db.Set<Designation>().AddRange(
    //            new Designation { Id = Guid.NewGuid(), TenantId = tenantId, Name = "Teacher" },
    //            new Designation { Id = Guid.NewGuid(), TenantId = tenantId, Name = "Principal" },
    //            new Designation { Id = Guid.NewGuid(), TenantId = tenantId, Name = "Accountant" }
    //        );
    //    }
    //}

    //private static async Task SeedTimetableDefaults(DbContext db, Guid? tenantId)
    //{
    //    var exists = await db.Set<Weekday>()
    //        .AnyAsync(x => tenantId == null || x.TenantId == tenantId);

    //    if (!exists)
    //    {
    //        var days = new[]
    //        {
    //            "Monday","Tuesday","Wednesday","Thursday","Friday","Saturday"
    //        };

    //        foreach (var d in days)
    //        {
    //            db.Set<Weekday>().Add(new Weekday
    //            {
    //                Id = Guid.NewGuid(),
    //                TenantId = tenantId,
    //                Name = d
    //            });
    //        }
    //    }
    //}

    //private static async Task SeedHolidayCalendar(DbContext db, Guid? tenantId)
    //{
    //    var exists = await db.Set<Holiday>()
    //        .AnyAsync(x => tenantId == null || x.TenantId == tenantId);

    //    if (!exists)
    //    {
    //        db.Set<Holiday>().Add(new Holiday
    //        {
    //            Id = Guid.NewGuid(),
    //            TenantId = tenantId,
    //            Name = "School Opening",
    //            Date = DateTime.UtcNow.Date,
    //            IsActive = true
    //        });
    //    }
    //}

}
