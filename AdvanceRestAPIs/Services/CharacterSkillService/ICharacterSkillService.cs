using AdvanceRestAPIs.Dtos.Character;
using AdvanceRestAPIs.Dtos.CharacterSkill;
using AdvanceRestAPIs.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdvanceRestAPIs.Services.CharacterSkillService
{
    public interface ICharacterSkillService
    {

        Task<ServiceResponse<GetCharacterDto>> AddCharacterSkill(AddCharacterSkillDto newCharacterSkill);
    }
}
