using BuzzBuzzHomeAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace BuzzBuzzHomeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerInfoController : ControllerBase
    {
        private readonly TestContext _testContext = new TestContext();
        // GET: api/CustomerInfo
        [HttpGet]
        public List<Customer> Get()
        {
            return _testContext.tblCustomers.Select(x => new Customer { value = x.CustomerId, text = x.CustomerName }).ToList();
        }

        // GET api/CustomerInfo/5
        [HttpGet("{id}")]
        public CustomerData Get(int id)
        {
            CustomerData customerData = new CustomerData();
            customerData.Customer = _testContext.tblCustomers.Where(x => x.CustomerId == id).SingleOrDefault();
            customerData.Products = _testContext.tblProducts.Where(x => x.CustomerId == id && x.Deleted == false)
                .Select(y => new Product { Id = y.ProductId, ProductId = y.ProductId, ProductName = y.ProductName, ProductPrice = y.ProductPrice }).ToList();
            return customerData;
        }
    }

    public class Customer
    {
        public int value { get; set; }
        public string? text { get; set; }
    }

    public class CustomerData
    {
        public tblCustomer? Customer { get; set; }
        public List<Product>? Products { get; set; }
    }

    public class Product
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; } = null!;
        public double? ProductPrice { get; set; }
    }
}


