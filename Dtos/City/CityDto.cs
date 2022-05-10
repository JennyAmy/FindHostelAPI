using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HousingAPI.Dtos
{
    public class CityDto
    {
        public int CityId { get; set; }

        [Required(ErrorMessage ="City name is required")]
        [StringLength(50, MinimumLength = 3)]
        [RegularExpression(".*[a-zA-Z]+.*", ErrorMessage = "Numbers only are not allowed")]
        public string CityName { get; set; }

        [Required]
        public string Country { get; set; }
    }
}
