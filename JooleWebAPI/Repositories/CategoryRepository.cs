using JooleWebAPI.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JooleWebAPI.Repositories
{
    public class CategoryRepository : Repository<tblCategory>, ICategoryRepository
    {
        public CategoryRepository(JooleContext context)
            : base(context)
        {
        }
    }
}
