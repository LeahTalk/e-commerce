using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.Collections.Generic;
namespace ecommerce.Models
{
    public class Transaction
    {
        // auto-implemented properties need to match the columns in your table
        // the [Key] attribute is used to mark the Model property being used for your table's Primary Key
        [Key]
        public int TransactionId { get; set; }
        public int Quantity { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer {get;set;}
        public int ProductId { get; set; }
        public Product Product {get;set;}
        public DateTime CreatedAt {get;set;} = DateTime.Now;
        public DateTime UpdatedAt {get;set;} = DateTime.Now;
    }
}