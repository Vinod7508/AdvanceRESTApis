using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdvanceRestAPIs.Models
{
    public class Weapon
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Damage { get; set; }

        //every Character will only have one Weapon and vice versa.

        public int CharacterId { get; set; }
        public Character Character { get; set; }
    }
}
