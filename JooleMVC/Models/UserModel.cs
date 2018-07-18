using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JooleMVC.Models
{
    public class UserModel
    {
        public int User_ID { get; set; }
        public string User_Name { get; set; }
        public string User_Email { get; set; }
        public string User_Image { get; set; }
        public string User_Password { get; set; }
        public int Credential_ID { get; set; }
        public byte[] User_Image_SRC { get; set; }
    }
}