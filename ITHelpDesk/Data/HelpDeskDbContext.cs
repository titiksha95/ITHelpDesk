using System;
using System.Collections.Generic;
using ITHelpDesk.Models;
using Microsoft.EntityFrameworkCore;

namespace ITHelpDesk.Data;

public partial class HelpDeskDbContext : DbContext
{
    public HelpDeskDbContext(DbContextOptions<HelpDeskDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<HdDepartment> HdDepartments { get; set; }

    public virtual DbSet<HdEmployee> HdEmployees { get; set; }

    public virtual DbSet<HdTicket> HdTickets { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<HdDepartment>(entity =>
        {
            entity.HasKey(e => e.DepartmentId).HasName("PK__HD_Depar__B2079BED9E8AE7A8");
        });

        modelBuilder.Entity<HdEmployee>(entity =>
        {
            entity.HasKey(e => e.EmployeeId).HasName("PK__HD_Emplo__7AD04F1190D7EC56");

            entity.HasOne(d => d.Department).WithMany(p => p.HdEmployees)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_HD_Employees_Departments");
        });

        modelBuilder.Entity<HdTicket>(entity =>
        {
            entity.HasKey(e => e.TicketId).HasName("PK__HD_Ticke__712CC607405B0A68");

            entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getdate())", "DF_HD_Tickets_CreatedDate");
            entity.Property(e => e.Status).HasDefaultValue("Open", "DF_HD_Tickets_Status");

            entity.HasOne(d => d.Employee).WithMany(p => p.HdTickets)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_HD_Tickets_Employees");
        });
        modelBuilder.HasSequence<int>("item_counter")
            .StartsAt(10L)
            .IncrementsBy(10);

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
