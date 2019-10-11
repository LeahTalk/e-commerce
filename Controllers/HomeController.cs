using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ecommerce.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ecommerce.Controllers
{
    public class HomeController : Controller
    {

        private ProductContext dbContext;

        public HomeController(ProductContext context)
        {
            dbContext = context;
        }
        [HttpGet]
        [Route("/")]
        public IActionResult Index()
        {
            List<Customer> AllCustomers = dbContext.Customers.Take(3).ToList();
            ViewBag.customers = AllCustomers;
            List<Product> AllProducts = dbContext.Products.Take(5).ToList();
            ViewBag.products = AllProducts;
            List<Transaction> AllTransactions = dbContext.Transactions
                .Include(t => t.Customer)
                .Include(t => t.Product)
                .Take(3)
                .ToList();
            ViewBag.transactions = AllTransactions;
            return View();
        }

        [HttpGet]
        [Route("/products")]
        public IActionResult Products()
        {
            List<Product> AllProducts = dbContext.Products.ToList();
            AllProducts.Reverse();
            ViewBag.products = AllProducts;
            return View();
        }

        [HttpPost]
        [Route("/products/new")]
        public IActionResult CreateProduct(Product newProduct) {
            List<Product> AllProducts = dbContext.Products.ToList();
            AllProducts.Reverse();
            if(ModelState.IsValid) {
                dbContext.Add(newProduct);
                dbContext.SaveChanges();
                return RedirectToAction("Products");
                
            }
            ViewBag.products = AllProducts;
            return View("Products");
        }

        [HttpPost]
        [Route("/orders/create")]
        public IActionResult CreateTransaction(Transaction newTransaction) {
            Product selectedProduct = dbContext.Products.FirstOrDefault(u => u.ProductId == newTransaction.ProductId);
            if(selectedProduct.Stock < newTransaction.Quantity) {
                ModelState.AddModelError("Quantity", "There are not enough of those in stock!");
                List<Customer> AllCustomers = dbContext.Customers.ToList();
                ViewBag.customers = AllCustomers;
                List<Product> AllProducts = dbContext.Products.ToList();
                ViewBag.products = AllProducts;
                List<Transaction> AllTransactions = dbContext.Transactions
                    .Include(t => t.Customer)
                    .Include(t => t.Product)
                    .ToList();
                ViewBag.transactions = AllTransactions;
                return View("Orders");
            }
            dbContext.Add(newTransaction);
            selectedProduct.Stock -= newTransaction.Quantity;
            dbContext.SaveChanges();
            return RedirectToAction("Orders");
        }

        [HttpGet]
        [Route("/orders")]
        public IActionResult Orders()
        {
            List<Customer> AllCustomers = dbContext.Customers.ToList();
            ViewBag.customers = AllCustomers;
            List<Product> AllProducts = dbContext.Products.ToList();
            ViewBag.products = AllProducts;
            List<Transaction> AllTransactions = dbContext.Transactions
                .Include(t => t.Customer)
                .Include(t => t.Product)
                .ToList();
            ViewBag.transactions = AllTransactions;
            return View();
        }

        [HttpGet]
        [Route("/customers")]
        public IActionResult Customers()
        {
            List<Customer> AllCustomers = dbContext.Customers.ToList();
            AllCustomers.Reverse();
            ViewBag.customers = AllCustomers;
            return View();
        }

        [HttpPost]
        [Route("/customers/new")]
        public IActionResult CreateCustomer(Customer newCustomer){
            List<Customer> AllCustomers = dbContext.Customers.ToList();
            AllCustomers.Reverse();
            if(ModelState.IsValid) {
                if(dbContext.Customers.Any(u => u.Name == newCustomer.Name))
                {
                    ModelState.AddModelError("Name", "A customer with this name already exists!");
                    ViewBag.customers = AllCustomers;
                    return View("Customers");
                }
                dbContext.Add(newCustomer);
                dbContext.SaveChanges();
                return RedirectToAction("Customers");
                
            }
            ViewBag.customers = AllCustomers;
            return View("Customers");
        }

        [HttpGet]
        [Route("/customers/delete/{customerId}")]
        public IActionResult DeleteCustomer(int customerId) {
            var customer = dbContext.Customers.FirstOrDefault(u => u.CustomerId == customerId);
            if (customer == null) {
                return RedirectToAction("Customers");
            }
            dbContext.Customers.Remove(customer);
            dbContext.SaveChanges();
            return RedirectToAction("Customers");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
