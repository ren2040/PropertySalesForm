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

        public FormData()
        {
           
        }
    }
}