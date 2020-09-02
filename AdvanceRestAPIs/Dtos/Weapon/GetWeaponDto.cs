using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdvanceRestAPIs.Dtos.Weapon
{
    public class GetWeaponDto
    {

        //We create a new GetWeaponDto class that only consists of the Name and the Damage of the Weapon. No need for an Id or the Character here.
        public string Name { get; set; }
        public int Damage { get; set; }
    }
}
