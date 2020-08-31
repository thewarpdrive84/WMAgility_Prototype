using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WMAgility.Models;

namespace WMAgility.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Dog> Dogs { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<Practice> Practices { get; set; }
        public DbSet<Competition> Competitions { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
    }
}
