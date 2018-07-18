using JooleWebAPI.DTO;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JooleWebAPI.Interfaces
{
    public interface IProductListRepository : IRepository<tblProduct>
    {
        DbSet<tblProduct> GetProductList();
    }
}
