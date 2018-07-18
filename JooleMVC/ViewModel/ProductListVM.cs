using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JooleMVC.ViewModel
{
    public class ProductListVM
    {
        public string Sub1AirFlow_CFM { get; set; }
        public int Sub1AirFlow_CFM_Min { get; set; }
        public int Sub1AirFlow_CFM_Max { get; set; }
        public string Sub1Power_W { get; set; }
        public int Sub1Power_W_Min { get; set; }
        public int Sub1Power_W_Max { get; set; }
        public string Sub1OperatingVoltage_VAC { get; set; }
        public int Sub1OperatingVoltage_VAC_Min { get; set; }
        public int Sub1OperatingVoltage_VAC_Max { get; set; }
        public string Sub1FanSpeed_RPM { get; set; }
        public int Sub1FanSpeed_RPM_Min { get; set; }
        public int Sub1FanSpeed_RPM_Max { get; set; }
    }
}