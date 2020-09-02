using AdvanceRestAPIs.Dtos.Character;
using AdvanceRestAPIs.Dtos.Weapon;
using AdvanceRestAPIs.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdvanceRestAPIs.Services.WeaponService
{
    public interface IWeaponService
    {
        Task<ServiceResponse<GetCharacterDto>> AddWeapon(AddWeaponDto newWeapon);
    }
}
