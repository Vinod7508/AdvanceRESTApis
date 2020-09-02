using AdvanceRestAPIs.Data;
using AdvanceRestAPIs.Dtos.Character;
using AdvanceRestAPIs.Dtos.CharacterSkill;
using AdvanceRestAPIs.Models;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AdvanceRestAPIs.Services.CharacterSkillService
{
    public class CharacterSkillService : ICharacterSkillService
    {

        private readonly AppDBContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;




        public CharacterSkillService(AppDBContext context, IHttpContextAccessor httpContextAccessor, IMapper mapper)
        {
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _context = context;
        }


        public async Task<ServiceResponse<GetCharacterDto>> AddCharacterSkill(AddCharacterSkillDto newCharacterSkill)
        {
            ServiceResponse<GetCharacterDto> response = new ServiceResponse<GetCharacterDto>();
            try
            {

                //Character character = await _context.characters
                //                      .FirstOrDefaultAsync(c => c.Id == newCharacterSkill.CharacterId &&
                //                      c.User.Id == int.Parse(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)));

                //To receive all skills and also the related Weapon of the user, we have to include them.

                //We can start with the Weapon. After _context.Characters we add .Include(c => c.Weapon). 
               //The skills are getting a bit more interesting. 
                //Again we add .Include(), but first we access the CharacterSkills and after that we access the child property Skill of the CharacterSkills with .ThenInclude().
               // That way, we get every property from the character that is stored in the database.

                Character character = await _context.characters
                                     .Include(c => c.Weapon)
                                     .Include(c => c.CharacterSkills).ThenInclude(cs => cs.Skill)
                                     .FirstOrDefaultAsync(c => c.Id == newCharacterSkill.CharacterId &&
                                     c.User.Id == int.Parse(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)));


                // we add the usual null-check. So, if the character is null we set the Success state and the Message and return the response.
                if (character == null)
                {
                    response.Success = false;
                    response.Message = "Character not found.";
                    return response;
                }

                //Similar to the character, if we cannot find the skill with the given SkillId, we set the ServiceResponse and return it.

                Skill skill = await _context.Skills
                             .FirstOrDefaultAsync(s => s.Id == newCharacterSkill.SkillId);

                if (skill == null)
                {
                    response.Success = false;
                    response.Message = "Skill not found.";
                    return response;
                }

                CharacterSkill characterSkill = new CharacterSkill
                {
                    Character = character,
                    Skill = skill
                };

                await _context.CharacterSkills.AddAsync(characterSkill);
                await _context.SaveChangesAsync();

                response.Data = _mapper.Map<GetCharacterDto>(character);



            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }
            return response;
        }
    }
}
