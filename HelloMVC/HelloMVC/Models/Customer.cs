using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace HelloMVC.Models
{
    public class Customer
    {
        public string Id { get; set; }
        [Required]
        [StringLength(10, ErrorMessage ="Your name is too long")]
        [DisplayName("Customer Name")]
        public string Name { get; set; }
        public string Telephone { get; set; }
    }
}