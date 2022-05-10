using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HousingAPI.Dtos.PropertyDto
{
    public class PropertyListDto
    {
        public int PropertyId { get; set; }

        public int sellRent { get; set; }
        public string Name { get; set; }
        public string PropertyType { get; set; }
        public int BHK { get; set; }
        public string FurnishingType { get; set; }
        public string Price { get; set; }
        public int BuiltArea { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public bool ReadyToMove { get; set; }
        public DateTime EstPossessionOn { get; set; }
        public string Photo { get; set; }
    }
}
