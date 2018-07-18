using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JooleMVC.Models
{
    public class ProductListBySubCategory
    {
        public int SubCategoryID { get; set;}
        public List<int> ListOfProductID{ get; set;}
    }
}