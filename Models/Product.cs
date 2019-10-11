using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.Collections.Generic;
namespace ecommerce.Models
{
    public class Product
    {
        // auto-implemented properties need to match the columns in your table
        // the [Key] attribute is used to mark the Model property being used for your table's Primary Key
        [Key]
        public int ProductId { get; set; }
        // MySQL VARCHAR and TEXT types can be represeted by a string
        [Required(ErrorMessage = "Product must have a name!")]
        [Display(Name = "Product:")] 
        public string Name { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        public int Stock { get; set; }
        public DateTime CreatedAt {get;set;} = DateTime.Now;
        public DateTime UpdatedAt {get;set;} = DateTime.Now;
        public List<Transaction> Transactions {get;set;}

    }
}