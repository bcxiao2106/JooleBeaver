using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using JooleWebAPI.Interfaces;

namespace JooleWebAPI.Repositories
{
    public class TechSpecFilterRepository : Repository<tblTechSpecFilter>, ITechSpecFilterRepository
    {
        public TechSpecFilterRepository(JooleContext context) : base(context)
        {
        }
    }
}