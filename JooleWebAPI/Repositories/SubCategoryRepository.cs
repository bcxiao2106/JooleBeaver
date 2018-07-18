using JooleWebAPI.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JooleWebAPI.Repositories
{
    public class SubCategoryRepository : Repository<tblSubCategory>, ISubCategoryRepository
    {
        public SubCategoryRepository(JooleContext context)
            : base(context)
        {
        }

    }
}