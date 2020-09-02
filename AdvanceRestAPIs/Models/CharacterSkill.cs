using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdvanceRestAPIs.Models
{
    public class CharacterSkill
    {

        //We create a new C# class and call it CharacterSkill. To join skills and characters now, we have to add them as properties. So, we add a Character and a Skill

        //Additionally, we need a primary key for this entity. This will be a composite key of the Skill and the Character. 
        //To be able to do that, by convention we add a property CharacterId for the Character, and a property SkillId for the Skill.

        public int CharacterId { get; set; }
        public Character Character { get; set; }
        public int SkillId { get; set; }
        public Skill Skill { get; set; }

        //But that’s not the whole magic. We still have to tell Entity Framework Core that we want to use these two Ids as a composite primary key. We do that with the help of the Fluent API
    }
}
