﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JooleMVC.Models
{
    public class ProductProperty
    {
        public int Product_ID { get; set; }
        public int Property_ID { get; set; }
        public string Property_Name { get; set; }
        public string Value { get; set; }
        public bool IsType { get; set; }
        public bool IsTechSpec { get; set; }
    }
}