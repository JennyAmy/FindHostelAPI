using HousingAPI.DbContexts;
using HousingAPI.Interfaces;
using HousingAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HousingAPI.Repo
{
    public class PropertyRepository : IPropertyRepository
    {
        private readonly DataContext context;

        public PropertyRepository(DataContext context)
        {
            this.context = context;
        }

        public void AddProperty(Property property)
        {
            context.Properties.Add(property);
        }

        public void DeleteProperty(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Property>> GetPropertiesAsync(int sellRent)
        {
            var properties = await context.Properties
                .Include(p => p.PropertyType)
                .Include(p => p.City)
                .Include(p => p.FurnishingType)
                .Include(p => p.Photos)
                .Where(p => p.SellRent == sellRent).ToListAsync();
            return properties;
        }

        public async Task<Property> GetPropertyDetailAsync(int id)
        {
            var property = await context.Properties
                .Include(p => p.PropertyType)
                .Include(p => p.City)
                .Include(p => p.FurnishingType)
                .Include(p => p.Photos)
                .Where(p => p.PropertyId == id).FirstOrDefaultAsync();
            return property;  
        }

        public async Task<Property> GetPropertyByIdAsync(int id)
        {
            var property = await context.Properties
                .Include(p => p.Photos)
                .Where(p => p.PropertyId == id).FirstOrDefaultAsync();
            return property;
        }
    }
}
