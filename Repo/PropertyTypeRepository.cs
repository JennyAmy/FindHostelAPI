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
    public class PropertyTypeRepository : IPropertyTypeRepository
    {
        private readonly DataContext context;

        public PropertyTypeRepository(DataContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<PropertyType>> GetPropertyTypesAsync()
        {
            return await context.PropertyTypes.ToListAsync();
        }
    }
}
