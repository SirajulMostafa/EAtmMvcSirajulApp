using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EAtmMvcSirajulApp.Models
{
    public class TransactionModel
    {
      //  [ForeignKey("EatmAccount")]
        public int Id { get; set; }
       // public int EatmAccountId { get; set; }
        public double WithdrawalAmount { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? TransactionDate { get; set; }
        public  EatmAccountModel EatmAccountModel { get; set; }
        public int EatmAccountModelId { get; set; }

    }
}