using JooleWebAPI.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JooleWebAPI.Repositories
{
    public class ProductPropertyValueRepository:Repository<tblPropertyValue>, IProductPropertyValueRepository
    {
        public ProductPropertyValueRepository(JooleContext context)
            : base(context)
        {
        }
    }
}