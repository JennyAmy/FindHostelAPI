using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HousingAPI.Dtos
{
    public class PhotoDto
    {
        
        public string PublicId { get; set; }
        public string ImageUrl { get; set; }
        public bool IsPrimary { get; set; }

    }
}
