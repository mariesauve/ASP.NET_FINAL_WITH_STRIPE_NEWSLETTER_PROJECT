using KoolStuff.Data;
using KoolStuff.Models;
using Microsoft.AspNetCore.Mvc;
using Stripe.Checkout;
using System.Collections.Generic;
                            // Working with database
namespace KoolStuff.Controllers
{
    public class CheckoutController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CheckoutController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            List<Product> productList = _context.Product.ToList();
            return View(productList);
        }

        //public List<Product> ProductList { get; private set; }

        //public CheckoutController()
        //{
        //    // Initialize ProductList
        //    ProductList = new List<Product>
        //{
        //    new Product
        //    {
        //        Image = "/BatmanT.jpg",
        //        Name = "Batman-T",
        //        Description = "Character T-Shirt",
        //        Price = 12,
        //        Quantity = 1
        //    },
        //    new Product
        //    {
        //        Image = "/LokiT.jpg",
        //        Name = "Loki-T",
        //        Description = "Character T-Shirt",
        //        Price = 17,
        //        Quantity = 2
        //    },
        //    new Product
        //    {
        //        Image = "/JokerT.jpg",
        //        Name = "Joker-T",
        //        Description = "Character T-Shirt",
        //        Price = 19,
        //        Quantity = 1
        //    }
        //};
        //}

        //public IActionResult Index()
        //{
        //    // You can directly return the ProductList
        //    return View(ProductList);
        //}

        public IActionResult Checkout()
        {
            // This uses the ProductList directly
            var domain = "http://localhost:5049/";
            var options = new SessionCreateOptions
            {
                SuccessUrl = domain + "Checkout/OrderConfirmation",
                CancelUrl = domain + "Checkout/Login",
                LineItems = new List<SessionLineItemOptions>(),
                Mode = "payment"
            };
            List<Product> productList = _context.Product.ToList();
            foreach (var item in productList)
            {
                int priceInCents = (int)(item.Price * 100);

                var sessionListItem = new SessionLineItemOptions
                {
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                        UnitAmount = priceInCents,
                        Currency = "cad",
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = item.Name.ToString(),
                        }
                    },
                    Quantity = item.Quantity
                };
                options.LineItems.Add(sessionListItem);
            }
            // This creates a new Stripe session using the provided options
            var service = new SessionService();
            Session session = service.Create(options);

            // This redirects to the Stripe page
            return Redirect(session.Url);  
        }
    }
}