using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HousingAPI.Models
{
    public class City : BaseEntity
    {
        public int CityId { get; set; }
        public string CityName { get; set; }

        public string Country { get; set; }

    }
}
