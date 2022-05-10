using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HousingAPI.Models
{
    public class User : BaseEntity
    {
        public int UserId { get; set; }

        [Required]
        public string Username { get; set; }


        [Required]
        public string Email { get; set; }

        [Required]
        public string Phone { get; set; }

        [Required]
        public byte[] Password { get; set; }

        public byte[] PasswordKey { get; set; }


    }
}
