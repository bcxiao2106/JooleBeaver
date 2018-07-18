using JooleWebAPI.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JooleWebAPI.Repositories
{
    public class ProductPropertyRepository:Repository<tblProperty>, IProductPropertyRepository
    {
        public ProductPropertyRepository(JooleContext context)
           : base(context)
        {
        }
    }
}