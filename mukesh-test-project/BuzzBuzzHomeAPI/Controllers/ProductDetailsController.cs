using Microsoft.AspNetCore.Mvc;
using BuzzBuzzHomeAPI.Models;

namespace BuzzBuzzHomeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductDetailsController : ControllerBase
    {
        private readonly TestContext _testContext = new TestContext();

        // POST api/<ProductDetails>
        [HttpPost]
        public int Post([FromBody] tblProduct product)
        {
            try
            {
                if (product.ProductId > 0)
                {
                    var productDb = _testContext.tblProducts.SingleOrDefault(x => x.ProductId == product.ProductId);
                    if (productDb != null)
                    {
                        productDb.ProductName = product.ProductName;
                        productDb.ProductPrice = product.ProductPrice;
                        _testContext.tblProducts.Update(productDb);
                        _testContext.SaveChanges();
                        return product.ProductId;
                    }
                    else
                    {
                        throw new Exception("Invalid Product ID!");
                    }
                }
                else
                {
                    tblProduct productNew = new tblProduct();
                    productNew.CustomerId = product.CustomerId;
                    productNew.ProductName = product.ProductName;
                    productNew.ProductPrice = product.ProductPrice;
                    _testContext.tblProducts.Add(productNew);
                    _testContext.SaveChanges();
                    return productNew.ProductId;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // DELETE api/<ProductDetails>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            try
            {
                var productDb = _testContext.tblProducts.SingleOrDefault(x => x.ProductId == id);
                if (productDb != null)
                {
                    productDb.Deleted = true;
                    _testContext.tblProducts.Update(productDb);
                    _testContext.SaveChanges();
                }
                else
                {
                    throw new Exception("Invalid Product ID!");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
