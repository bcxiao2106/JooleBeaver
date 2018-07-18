using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace JooleWebAPI.Interfaces
{
    public interface IProjectRepository : IRepository<tblProject>
    {
        DbSet<tblState> GetStatesList();
        DbSet<tblCity> GetCityList();
    }
}