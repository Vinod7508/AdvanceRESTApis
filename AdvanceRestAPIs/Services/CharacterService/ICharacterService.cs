﻿using AdvanceRestAPIs.Dtos.Character;
using AdvanceRestAPIs.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdvanceRestAPIs.Services.CharacterService
{
   public interface ICharacterService
    {
        Task<ServiceResponse<List<GetCharacterDto>>> GetAllCharacters(int Userid);
        Task<ServiceResponse<GetCharacterDto>> GetCharacterById(int id);
        Task<ServiceResponse<List<GetCharacterDto>>> AddCharacter(AddCharacterDto newCharacter);
        Task<ServiceResponse<GetCharacterDto>> UpdateCharacter(UpdateCharacterDto updatedCharacter);

        Task<ServiceResponse<List<GetCharacterDto>>> DeleteCharacter(int id);

        


    }
}
