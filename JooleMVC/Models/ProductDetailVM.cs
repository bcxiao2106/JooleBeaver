using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JooleMVC.Models
{
    public class ProductDetailVM
    {
        public List<ProductDetails> ProductList { get; set; }
        public UserModel User {get; set;}
        public string[] data { get; set; }
    }
}