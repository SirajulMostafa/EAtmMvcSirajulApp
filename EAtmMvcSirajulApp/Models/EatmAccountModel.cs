using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EAtmMvcSirajulApp.Models
{
    public class EatmAccountModel
    {    [Key]
        public int Id { get; set; }
        [Required]
       // [DataType(DataType.Password)]
        [Display(Name = "Card Number")]
        public int CardNumber { get; set; }
        [Required]
        [Display(Name = "Pin Number")]
        public int PinNumber { get; set; }
        [Required]
        [Display(Name = "Balance")]
        public double Balance { get; set; }


    }
}