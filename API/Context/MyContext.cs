using API.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Context
{
    public class MyContext : DbContext //ORM yg akan digunakan (Entity Framework)
    {
        public MyContext(DbContextOptions<MyContext> options) : base(options)
        { 
        
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionBuilder) 
        {
            optionBuilder.UseLazyLoadingProxies();
        }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<University> Universities { get; set; }
        public DbSet<Education> Educations { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Profiling> Profilings { get; set; }
        public DbSet<AccountRole> AccountRoles { get; set; }
        public DbSet<Role> Roles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Many to One
            modelBuilder.Entity<University>()
                .HasMany(e => e.Educations)
                .WithOne(u => u.Univerisity);
            //One to One
            modelBuilder.Entity<Employee>()
                .HasOne(a => a.Account)
                .WithOne(e => e.Employee)
                .HasForeignKey<Account>(a => a.NIK);
            //One to Many
            modelBuilder.Entity<Profiling>()
                .HasOne(e => e.Education)
                .WithMany(p => p.Profilings);
            //One to one
            modelBuilder.Entity<Account>()
               .HasOne(p => p.Profiling)
               .WithOne(a => a.Account)
                .HasForeignKey<Profiling>(p => p.NIK);

            modelBuilder.Entity<AccountRole>()
                .HasOne(a => a.Account)
                .WithMany(ar => ar.AccountRoles);

            modelBuilder.Entity<AccountRole>()
                .HasOne(r => r.Role)
                .WithMany(ar => ar.AccountRoles);

        }
    }
}
