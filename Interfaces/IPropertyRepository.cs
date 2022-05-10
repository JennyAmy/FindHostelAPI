using HousingAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HousingAPI.Interfaces
{
    public interface IPropertyRepository
    {
        Task<IEnumerable<Property>> GetPropertiesAsync(int sellRent);

        Task<Property> GetPropertyDetailAsync(int id);

        Task<Property> GetPropertyByIdAsync(int id);
        void AddProperty(Property property);
        void DeleteProperty(int id); 
    }
}
