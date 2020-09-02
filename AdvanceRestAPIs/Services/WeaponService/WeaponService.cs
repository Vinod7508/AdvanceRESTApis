using AdvanceRestAPIs.Data;
using AdvanceRestAPIs.Dtos.Character;
using AdvanceRestAPIs.Dtos.Weapon;
using AdvanceRestAPIs.Models;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AdvanceRestAPIs.Services.WeaponService
{
    public class WeaponService : IWeaponService
    {

        private readonly AppDBContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;

        public WeaponService(AppDBContext context, IHttpContextAccessor httpContextAccessor, IMapper mapper)
        {
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _context = context;
        }



        public async Task<ServiceResponse<GetCharacterDto>> AddWeapon(AddWeaponDto newWeapon)
        {
            ServiceResponse<GetCharacterDto> response = new ServiceResponse<GetCharacterDto>();
            try
            {
                //Now for the try block, we first get the correct Character from the database. 
                //We access the Characters from the _context, find the first entity with the given CharacterId and also the correct User so that we know this character really belongs to the currently authorized user.


                //Just to recap, we get the Id of the current user by accessing the NameIdentifier claims value from the JSON web token.
                Character character = await _context.characters
                    .FirstOrDefaultAsync(c => c.Id == newWeapon.CharacterId &&
                    c.User.Id == int.Parse(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)));



                //When the character is null, something is wrong and we return a failing response.
                if (character == null)
                {
                    response.Success = false;
                    response.Message = "Character not found.";
                    return response;
                }


                //if we got the proper character, we can create a new Weapon instance, with the given Name and Damage value and also set the Character property of this new Weapon instance to the character object we got from the database.
                //By the way, we could have added a new mapping from the AddWeaponDto to the Weapon type, or we just set these two properties manually here
                Weapon weapon = new Weapon
                {
                    Name = newWeapon.Name,
                    Damage = newWeapon.Damage,
                    Character = character
                };



                //After that, we add this new weapon to the database, save the changes and return the character.
                await _context.Weapons.AddAsync(weapon);
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
