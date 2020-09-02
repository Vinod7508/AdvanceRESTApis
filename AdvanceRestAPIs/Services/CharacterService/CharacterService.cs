using AdvanceRestAPIs.Data;
using AdvanceRestAPIs.Dtos.Character;
using AdvanceRestAPIs.Models;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AdvanceRestAPIs.Services.CharacterService
{
    public class CharacterService : ICharacterService
    {
        //we can inject the IHttpContextAccessor in the CharacterService and access the current user from there.

        private readonly IHttpContextAccessor _httpContextAccessor;

        private readonly AppDBContext _context;
        private readonly IMapper _mapper;

        public CharacterService(IMapper mapper, AppDBContext context, IHttpContextAccessor httpContextAccessor)
        {
            _mapper = mapper;
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }


    private static List<Character> characters = new List<Character> {
    new Character(),
    new Character { Id = 1, Name = "Sam"}

    };


        //Since we need the Id of the authenticated user quite often, let’s write a method for that and call it GetUserId().
        private int GetUserId() => int.Parse(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));



        public async Task<ServiceResponse<List<GetCharacterDto>>> AddCharacter(AddCharacterDto newCharacter)
        {
            ServiceResponse<List<GetCharacterDto>> serviceResponse = new ServiceResponse<List<GetCharacterDto>>();
            Character character = _mapper.Map<Character>(newCharacter);

            // we set the character.User by grabbing the User object from the database with await _context.Users.FirstOrDefaultAsync() and as condition, we want the user with the Id of the authenticated user.
            character.User = await _context.User.FirstOrDefaultAsync(u => u.Id == GetUserId());

            await _context.characters.AddAsync(character);
            await _context.SaveChangesAsync();
            serviceResponse.Data = (_context.characters.Where(c => c.User.Id == GetUserId()).Select(c => _mapper.Map<GetCharacterDto>(c))).ToList();
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> DeleteCharacter(int id)
        {
            ServiceResponse<List<GetCharacterDto>> serviceResponse = new ServiceResponse<List<GetCharacterDto>>();
            try
            {
                Character character = await _context.characters.FirstOrDefaultAsync(c => c.Id == id && c.User.Id == GetUserId());
                if (character != null)
                {
                    _context.characters.Remove(character);
                    await _context.SaveChangesAsync();
                    serviceResponse.Data = (_context.characters.Where(c => c.User.Id == GetUserId())
                        .Select(c => _mapper.Map<GetCharacterDto>(c))).ToList();
                }
                else
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "Character not found.";
                }
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> GetAllCharacters()
        {
            ServiceResponse<List<GetCharacterDto>> serviceResponse = new ServiceResponse<List<GetCharacterDto>>();

            // List<Character> dbCharacters = await _context.characters.Where(c => c.User.Id == userid).ToListAsync();

            List<Character> dbCharacters = await _context.characters.Where(c => c.User.Id == GetUserId()).ToListAsync();

            serviceResponse.Data = dbCharacters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToList();
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetCharacterDto>> GetCharacterById(int id)
        {
            ServiceResponse<GetCharacterDto> serviceResponse = new ServiceResponse<GetCharacterDto>();
            Character dbCharacter = await _context.characters.FirstOrDefaultAsync(c => c.Id == id && c.User.Id == GetUserId());
            serviceResponse.Data = _mapper.Map<GetCharacterDto>(dbCharacter);
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetCharacterDto>> UpdateCharacter(UpdateCharacterDto updatedCharacter)
        {
            ServiceResponse<GetCharacterDto> serviceResponse = new ServiceResponse<GetCharacterDto>();
             try
            {
                //Character character = characters.FirstOrDefault(c => c.Id == updatedCharacter.Id);
                Character character = await _context.characters.Include(c => c.User).FirstOrDefaultAsync(c => c.Id == updatedCharacter.Id);

                if(character.User.Id == GetUserId())
                {
                    character.Name = updatedCharacter.Name;
                    character.Class = updatedCharacter.Class;
                    character.Defense = updatedCharacter.Defense;
                    character.HitPoints = updatedCharacter.HitPoints;
                    character.Intelligence = updatedCharacter.Intelligence;
                    character.Strength = updatedCharacter.Strength;


                    _context.characters.Update(character);
                    await _context.SaveChangesAsync();

                    serviceResponse.Data = _mapper.Map<GetCharacterDto>(character);
                }
                else
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "Character not found.";
                }

              }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }
    }

}
