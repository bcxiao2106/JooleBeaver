using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JooleWebAPI.DTO
{
    public class ProjectDTO
    {
        public int Project_ID { get; set; }
        public string Project_Name { get; set; }
        public int User_ID { get; set; }
        public string Project_Address1 { get; set; }
        public string Project_Address2 { get; set; }
        public int Project_City { get; set; }
        public int Project_State { get; set; }
        public int Project_Size { get; set; }
        public string Client_Name { get; set; }
    }
}