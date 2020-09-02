using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdvanceRestAPIs.Models
{
    public class Skill
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Damage { get; set; }

        
        //for many to many reletionship
        public List<CharacterSkill> CharacterSkills { get; set; }

        //Notice, that we don’t add a list of type Character here. 
        //We would do that if we wanted to implement a one-to-many relation, but for a many-to-many relationship, we need a special implementation - and that would be a joining table.
    }
}
