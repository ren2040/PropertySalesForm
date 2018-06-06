using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace Property_Sale_Reservation_Form.Models
{
    public class FormData
    {
        [Required]
     public string addressLine1 { get; set; }
     public string addressLine2 { get; set; }
     public string city { get; set; }
     public string state { get; set; }

     public string zip { get; set; }
     public float amount { get; set; }
     public string firstName { get; set; }
     public string lastName { get; set; }    
     public string stage2 { get; set; }

        public FormData()
        {
           
        }
    }
}