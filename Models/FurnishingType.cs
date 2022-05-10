using System.ComponentModel.DataAnnotations;

namespace HousingAPI.Models
{
    public class FurnishingType : BaseEntity
    {
        public int FurnishingTypeId { get; set; }
        [Required]
        public string Name { get; set; }

    }
}