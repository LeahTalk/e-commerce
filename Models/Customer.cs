using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.Collections.Generic;
namespace ecommerce.Models
{
    public class Customer
    {
        // auto-implemented properties need to match the columns in your table
        // the [Key] attribute is used to mark the Model property being used for your table's Primary Key
        [Key]
        public int CustomerId { get; set; }
        // MySQL VARCHAR and TEXT types can be represeted by a string
        [Required(ErrorMessage = "Customer must have a name!")]
        [Display(Name = "Name:")] 
        public string Name { get; set; }
        public DateTime CreatedAt {get;set;} = DateTime.Now;
        public DateTime UpdatedAt {get;set;} = DateTime.Now;
        public List<Transaction> CreatedTransactions {get;set;}

    }
}