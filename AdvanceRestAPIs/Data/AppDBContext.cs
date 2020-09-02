using AdvanceRestAPIs.Models;
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

        public DbSet<Skill> Skills { get; set; }
        public DbSet<CharacterSkill> CharacterSkills { get; set; }



        //We override the method OnModelCreating(). This method takes a ModelBuilder argument, which “defines the shape of your entities, the relationships between them and how they map to the database”. Exactly what we need.
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            //The only thing we have to configure here is the composite key of the CharacterSkill entity which consists of the CharacterId and the SkillId. 
            //We do that with modelBuilder.Entity<CharacterSkill>().HasKey(cs => new { cs.CharacterId, cs.SkillId });.
            modelBuilder.Entity<CharacterSkill>()
                .HasKey(cs => new { cs.CharacterId, cs.SkillId });
        }


    }
}
