using AdvanceRestAPIs.Dtos.Character;
using AdvanceRestAPIs.Dtos.Skill;
using AdvanceRestAPIs.Dtos.Weapon;
using AdvanceRestAPIs.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdvanceRestAPIs
{
    public class AutoMapperProfile : Profile
    {

        public AutoMapperProfile()
        {
            //CreateMap<Character, GetCharacterDto>();
            CreateMap<AddCharacterDto, Character>();
            CreateMap<Weapon, GetWeaponDto>();

            CreateMap<Skill, GetSkillDto>();

            //we want to access the skills of a character directly, without displaying the joining entity CharacterSkill.
            // We can do that with the help of AutoMapper and the help of the Select() function.


            CreateMap<Character, GetCharacterDto>()
            .ForMember(dto => dto.Skills, c => c.MapFrom(c => c.CharacterSkills.Select(cs => cs.Skill)));
            //First we utilize the ForMember() function for the <Character, GetCharacterDto>-Map. With this function, we can define a special mapping for a specific member of the mapped type.
            //In our case, we properly want to set the Skills of the DTO.
            //To do that, we access the Character object and from that object - hence the function MapFrom() - we grab the CharacterSkills and select the Skill from every CharacterSkill.
            //That’s how we make the jump to the skills directly.
        }
    }
}
