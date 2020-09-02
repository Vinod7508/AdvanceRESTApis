using AdvanceRestAPIs.Dtos.Weapon;
using AdvanceRestAPIs.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdvanceRestAPIs.Dtos.Character
{
    public class GetCharacterDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = "Frodo";
        public int HitPoints { get; set; } = 100;
        public int Strength { get; set; } = 10;
        public int Defense { get; set; } = 10;
        public int Intelligence { get; set; } = 10;
        public RpgClass Class { get; set; } = RpgClass.Knight;


        //we add the Weapon property of type GetWeaponDto to the GetCharacterDto. for one to one reltionship
        public GetWeaponDto Weapon { get; set; }
    }
}
