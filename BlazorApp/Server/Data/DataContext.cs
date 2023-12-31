﻿using BlazorApp.Shared;
using BlazorApp.Shared.UserManagement;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;

namespace BlazorAADE.Server.Data
{
    public class DataContext : DbContext
    {

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<User> Users { get; set; }

    }
}
