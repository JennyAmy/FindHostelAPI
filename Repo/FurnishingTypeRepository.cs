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
    public class FurnishingTypeRepository : IFurnishingTypeRepository
    {
        private readonly DataContext context;

        public FurnishingTypeRepository(DataContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<FurnishingType>> GetFurnishingTypesAsync()
        {
            return await context.FurnishingTypes.ToListAsync();
        }
    }
}
