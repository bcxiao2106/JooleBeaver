using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using JooleWebAPI.Interfaces;

namespace JooleWebAPI.Repositories
{
    public class ProductToProjectRepository:Repository<tblProjToProd>, IProductToProjectRepository
    {
        public ProductToProjectRepository(JooleContext context)
            : base(context)
        {
        }
    }
}