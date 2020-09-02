using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdvanceRestAPIs.Dtos.Skill
{
    public class GetSkillDto
    {

        //we create the DTO GetSkillDto, because we only need that one to display the skills of a character. The properties we need are Name and Damage.
        public string Name { get; set; }
        public int Damage { get; set; }
    }
}
