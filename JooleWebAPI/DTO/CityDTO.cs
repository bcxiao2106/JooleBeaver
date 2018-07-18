using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace JooleWebAPI.DTO
{
    public class CityDTO
    {
        public int City_ID { get; set; }
        public string City_Name { get; set; }
        public int State_ID { get; set; }
    }
}