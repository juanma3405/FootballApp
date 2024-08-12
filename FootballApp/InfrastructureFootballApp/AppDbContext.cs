using System;
using System.Collections.Generic;
using System.Linq;
using BusinessLogicFootballApp.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace InfrastructureFootballApp
{
    public class AppDbContext: IdentityDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<League> League { get; set; }
        public DbSet<Matchdays> Matchdays { get; set; }

        /*protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Ignore<League>();
            builder.Ignore<Matchdays>();
        }*/
    }
}
