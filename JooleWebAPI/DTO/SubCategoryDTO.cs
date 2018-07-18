using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JooleWebAPI.DTO
{
    public class SubCategoryDTO
    {
        public int SubCategory_ID { get; set; }
        public int Category_ID { get; set; }
        public string SubCategory_Name { get; set; }
    }
}