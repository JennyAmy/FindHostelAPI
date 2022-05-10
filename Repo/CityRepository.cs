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
    public class CityRepository : ICityRepository
    {
        private readonly DataContext context;

        public CityRepository(DataContext context)
        {
            this.context = context;
        }
        public void AddCity(City city)
        {
             context.Cities.AddAsync(city);      
        }

        public void DeleteCity(int CityId)
        {
            var city = context.Cities.Find(CityId);
            context.Cities.Remove(city);
        }

        public async Task<City> FindCity(int id)
        {
            return await context.Cities.FindAsync(id);
        }

        public async Task<IEnumerable<City>> GetCitiesAsync()
        {
            return await context.Cities.ToListAsync();
        }
    }
}
