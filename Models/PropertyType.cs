using System.ComponentModel.DataAnnotations;

namespace HousingAPI.Models
{
    public class PropertyType : BaseEntity
    {
        public int PropertyTypeId { get; set; }
        [Required]
        public string Name { get; set; }

    }
}