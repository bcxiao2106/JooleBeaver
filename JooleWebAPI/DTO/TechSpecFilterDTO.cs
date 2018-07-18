using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JooleWebAPI.DTO
{
    public class TechSpecFilterDTO
    {
        public int Property_ID { get; set; }
        public string Property_Name { get; set; }
        public int SubCategory_ID { get; set; }
        public int Min_Value { get; set; }
        public int Max_Value { get; set; }
    }
}