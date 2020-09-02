﻿using AdvanceRestAPIs.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdvanceRestAPIs.Data
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
        {}

        public DbSet<Character> characters { get; set; }
        public DbSet<User> User { get; set; }

        public DbSet<Weapon> Weapons { get; set; }
    }
}
