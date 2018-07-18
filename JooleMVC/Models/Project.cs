using System.ComponentModel.DataAnnotations;

namespace JooleMVC.Models
{
    public class Project
    {
        [Required]
        public int Project_ID { get; set; }

        [StringLength(50), Required]
        public string Project_Name { get; set; }

        [Required]
        public int User_ID { get; set; }

        [StringLength(100), Required]
        public string Project_Address1 { get; set; }

        [StringLength(100)]
        public string Project_Address2 { get; set; }

        [Required]
        public int Project_City { get; set; }

        [Required]
        public int Project_State { get; set; }

        [Required]
        public int Project_Size { get; set; }

        [StringLength(50), Required]
        public string Client_Name { get; set; }

    }
}