using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdvanceRestAPIs.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }


        //Users can have several characters but not the other way around. An RPG character can only have one user.
        public List<Character> Characters { get; set; }
    }
}
