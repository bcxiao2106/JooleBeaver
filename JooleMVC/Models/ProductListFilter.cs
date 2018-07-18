using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JooleMVC.Models
{
    public class ProductListFilter
    {
        public int Property_ID { get; set; }
        public string Property_Name { get; set; }
        public int MinValue { get; set; }
        public int MaxValue { get; set; }
    }
}