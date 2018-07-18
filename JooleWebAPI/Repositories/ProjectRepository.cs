using JooleWebAPI.Interfaces;
using System.Data.Entity;

namespace JooleWebAPI.Repositories
{
    public class ProjectRepository : Repository<tblProject>, IProjectRepository
    {
        public ProjectRepository(JooleContext context)
            : base(context)
        {
        }

        public DbSet<tblState> GetStatesList()
        {
            return this.Context.tblStates;
        }

        public DbSet<tblCity> GetCityList()
        {
            return this.Context.tblCities;
        }

        public DbSet<tblCategory> GetCategoryList()
        {
            return this.Context.tblCategories;
        }
    }
}