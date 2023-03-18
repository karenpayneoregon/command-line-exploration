﻿
// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable

using CombinedConfigDemo.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Environment = CommandArgsConsoleApp1.Classes.Environment;

namespace CommandArgsConsoleApp1.Data;

public partial class LogContext : DbContext
{
    public LogContext()
    {
    }
    private readonly Environment _environment;
    public LogContext(Environment environment)
    {
        _environment = environment;
    }
    public LogContext(DbContextOptions<LogContext> options)
        : base(options)
    {
    }

    public virtual DbSet<LogEvents> LogEvents { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        IConfigurationRoot configuration = Classes.Configurations.GetConfigurationRoot(_environment.ToString());
        optionsBuilder.UseSqlServer(configuration.GetSection("ConnectionStrings")["LogDatabase"]);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<LogEvents>(entity =>
        {
            entity.Property(e => e.Timestamp).HasColumnType("datetime");
        });

        modelBuilder.Entity<LogEvents>().OwnsOne(
            owner => owner.LogEvent, ownedNavigationBuilder =>
            {
                ownedNavigationBuilder.ToJson();
            });

        OnModelCreatingPartial(modelBuilder);

    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}