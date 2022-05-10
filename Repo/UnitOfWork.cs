using HousingAPI.DbContexts;
using HousingAPI.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HousingAPI.Repo
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext context;

        public UnitOfWork(DataContext context)
        {
            this.context = context;
        }
          
        public ICityRepository CityRepository =>
            new CityRepository(context);

        public IUserRepository UserRepository =>
             new UserRepository(context);

        public IPropertyRepository PropertyRepository =>
            new PropertyRepository(context);

        public IPropertyTypeRepository PropertyTypeRepository =>
             new PropertyTypeRepository(context);

        public IFurnishingTypeRepository FurnishingTypeRepository =>
             new FurnishingTypeRepository(context);

        public async Task<bool> SaveAsync()
        { 
            return await context.SaveChangesAsync() > 0;
        }
    }
}
