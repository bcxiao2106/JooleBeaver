using JooleWebAPI.Interfaces;
using JooleWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace JooleWebAPI.Repositories
{
    public class UserRepository : Repository<tblUser>, IUserRepository
    {
        JooleContext Context1;
        public UserRepository(JooleContext context)
            : base(context)
        {
            Context1 = context;
        }
        public bool userNameCheck(string userName)
        {
            // int count = 0;
            int count = (from ret in Context1.tblUsers
                         where ret.User_Name == userName
                         select ret.User_Name).Count();
            return (count > 0);
        }
        public bool emailCheck(string emailID)
        {
            // int count = 0;
            int count = (from ret in Context1.tblUsers
                         where ret.User_Email == emailID
                         select ret.User_Email).Count();
            return (count > 0);
        }
        public string loginCheck(string userName, string password)
        {
            // int count = 0;
            int count = (from ret in Context1.tblUsers
                         where ret.User_Password == password && (ret.User_Name == userName || ret.User_Email == userName)
                         select ret.User_ID).Count();
            if (count > 0)
            {
                var tmp = (from ret in Context1.tblUsers
                           where ret.User_Name == userName && ret.User_Password == password
                           select new
                           {
                               ret.User_ID,
                               ret.User_Name,
                               ret.Credential_ID
                           }).ToList();
                string ret1 = null;
                foreach (var x in tmp) { ret1 += x.ToString(); }
                return ret1;
            }
            else
            {
                return "Not Found!";
            }
        }
        public List<byte[]> GetImageSRC(string userName)
        {
            var ab = (
            from ret in Context1.tblUsers
            where ret.User_Name == userName
            select ret.User_Image_SRC).ToList();
            return ab;
        }

        public string GetProductDescription(int productID)
        {
            var tmp = (from ret1 in Context1.tblManufacturers
                       join ret2 in Context1.tblProducts
                       on ret1.Manufacturer_ID equals ret2.Manufacturer_ID
                       where ret2.Product_ID == productID
                       select new
                       {
                           ret1.Manufacturer_Name,
                           ret2.Series,
                           ret2.Model
                       }).ToList();
            string ret = null;
            foreach (var x in tmp) { ret += x.ToString(); }
            return ret;
        }
        public string GetType(int productID)
        {
            var tmp = (from ret1 in Context1.tblPropertyValues
                       join ret2 in Context1.tblProducts
                       on ret1.Product_ID equals ret2.Product_ID
                       join ret3 in Context1.tblProperties
                       on ret1.Property_ID equals ret3.Property_ID
                       where ret3.IsType == true && ret2.Product_ID == productID
                       select new
                       {
                           ret3.Property_Name,
                           ret1.Value
                       }).ToList();
            string ret = null;
            foreach (var x in tmp) { ret += x.ToString(); }
            return ret;
        }
        public string GetTechSpecs(int productID)
        {
            var tmp = (from ret1 in Context1.tblPropertyValues
                       join ret2 in Context1.tblProducts
                       on ret1.Product_ID equals ret2.Product_ID
                       join ret3 in Context1.tblProperties
                       on ret1.Property_ID equals ret3.Property_ID
                       where ret3.IsTechSpec == true && ret2.Product_ID == productID
                       select new
                       {
                           ret3.Property_Name,
                           ret1.Value
                       }).ToList();
            string ret = null;
            foreach (var x in tmp) { ret += x.ToString(); }
            return ret;
        }
        public string GetSeriesInfo(int productID)
        {
            var tmp = (from ret1 in Context1.tblProducts
                       where ret1.Product_ID == productID
                       select new
                       {
                           ret1.Series_Info
                       }).ToList();
            string ret = null;
            foreach (var x in tmp) { ret += x.ToString(); }
            return ret;
        }
        public string GetProductDoc(int productID)
        {
            var tmp = (from ret1 in Context1.tblDocuments
                       join ret2 in Context1.tblProducts
                       on ret1.Document_ID equals ret2.Document_ID
                       where ret2.Product_ID == productID
                       select new
                       {
                           ret1.Document_Folder_Path
                       }).ToList();
            string ret = null;
            foreach (var x in tmp) { ret += x.ToString(); }
            return ret;
        }
        public string GetSalesRep(int productID)
        {
            var tmp = (from ret1 in Context1.tblSales
                       join ret2 in Context1.tblProducts
                       on ret1.Sales_ID equals ret2.Sales_ID
                       where ret2.Product_ID == productID
                       select new
                       {
                           ret1.Sales_Name,
                           ret1.Sales_Phone,
                           ret1.Sales_Email,
                           ret1.Sales_Web
                       }).ToList();
            string ret = null;
            foreach (var x in tmp) { ret += x.ToString(); }
            return ret;
        }
        public string GetManufacturer(int productID)
        {
            var tmp = (from ret1 in Context1.tblDepartments
                       join ret2 in Context1.tblManufacturers
                       on ret1.Manufacturer_ID equals ret2.Manufacturer_ID
                       join ret3 in Context1.tblProducts
                       on ret1.Manufacturer_ID equals ret3.Manufacturer_ID
                       where ret3.Product_ID == productID
                       select new
                       {
                           ret1.Department_Name,
                           ret1.Department_Phone,
                           ret1.Department_Email,
                           ret2.Manufacturer_Web
                       }).ToList();
            string ret = null;
            foreach (var x in tmp) { ret += x.ToString(); }
            return ret;
        }
    }
}