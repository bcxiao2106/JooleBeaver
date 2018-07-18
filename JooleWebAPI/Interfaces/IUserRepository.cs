using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JooleWebAPI.Interfaces
{
    public interface IUserRepository : IRepository<tblUser>
    {
        bool userNameCheck(string userName);
        bool emailCheck(string emailID);
        string loginCheck(string userName, string password);
        List<byte[]> GetImageSRC(string userName);
        string GetType(int productID);
        string GetProductDescription(int productID);
        string GetTechSpecs(int productID);
        string GetSeriesInfo(int productID);
        string GetProductDoc(int productID);
        string GetSalesRep(int productID);
        string GetManufacturer(int productID);
    }
}
