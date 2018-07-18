using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JooleMVC.Models
{
    public class TechSpecSubID_1
    {
        public string AirFlow_CFM { get; set; }
        public int AirFlow_CFM_Min { get; set; }
        public int AirFlow_CFM_Max { get; set; }
        public string Power_W { get; set; }
        public int Power_W_Min { get; set; }
        public int Power_W_Max { get; set; }
        public string OperatingVoltage_VAC { get; set; }
        public int OperatingVoltage_VAC_Min { get; set; }
        public int OperatingVoltage_VAC_Max { get; set; }
        public string FanSpeed_RPM { get; set; }
        public int FanSpeed_RPM_Min { get; set; }
        public int FanSpeed_RPM_Max { get; set; }

    }
}