using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EAtmMvcSirajulApp.Models
{
    public class TransactionModel
    {
        [Key]
        public int Id { get; set; }
        public int EatmAccountId { get; set; }
        public double Balance { get; set; }

    }
}