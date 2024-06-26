﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace FootballAppV2.Models
{
    //public class AppDbContext: DbContext
    public class AppDbContext : IdentityDbContext
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
