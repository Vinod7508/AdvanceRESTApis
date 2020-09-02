using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdvanceRestAPIs.Dtos.CharacterSkill;
using AdvanceRestAPIs.Services.CharacterSkillService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AdvanceRestAPIs.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class CharacterSkillController : ControllerBase
    {

        private readonly ICharacterSkillService _characterSkillService;
        public CharacterSkillController(ICharacterSkillService characterSkillService)
        {
            _characterSkillService = characterSkillService;
        }


        [HttpPost]
        public async Task<IActionResult> AddCharacterSkill(AddCharacterSkillDto newCharacterSkill)
        {
            return Ok(await _characterSkillService.AddCharacterSkill(newCharacterSkill));
        }



    }
}
