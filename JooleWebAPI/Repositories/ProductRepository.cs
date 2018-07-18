using JooleWebAPI.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JooleWebAPI.Repositories
{
    public class ProductRepository:Repository<tblProduct>, IProductRepository
    {
        public ProductRepository(JooleContext context)
           : base(context)
        {
        }
    }
}