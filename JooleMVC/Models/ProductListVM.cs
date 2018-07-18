using JooleMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JooleMVC.Models
{
    public class ProductListVM
    {
        public List<ProductDetails> ProductList { get; set; }
        public List<ProductListFilter> ProductListFilter { get; set; }
        public UserModel User { get; set; }
    }
}