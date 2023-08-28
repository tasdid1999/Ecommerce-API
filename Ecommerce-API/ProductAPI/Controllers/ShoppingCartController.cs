using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductAPI.Model.Domain;
using ProductAPI.Repository;

namespace ProductAPI.Controllers
{
    [Route("api/")]
    [ApiController]
    public class ShoppingCartController : ControllerBase
    {
        private readonly IShoppingCartRepository shoppingCartRepo;
        public ShoppingCartController(IShoppingCartRepository _shoppingCartRepository)
        {
            shoppingCartRepo = _shoppingCartRepository;
        }
        [HttpPost("cart")]
        public async Task<IActionResult> AddToCart([FromQuery]int userId ,[FromQuery] int productId)
        {
            try
            {
               var isAdded =  await shoppingCartRepo.AddToCartAsync(new ShoppingCart { UserId = userId, ProductId = productId });

               if(isAdded)
                {
                    return Ok(new { Ok = true , message = "item added succesfully" });

                }

               return BadRequest(new { ok = false , message = "something went wrong!" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { ok = false, ex.Message });
            }
        }

        [HttpGet("cart")]
        public async Task<IActionResult> GetItemOfCart([FromQuery]int userId)
        {
            try
            {
                var listOfCartedProduct = await shoppingCartRepo.GetCartItemsAsync(userId);

                if (listOfCartedProduct == null)
                {
                    return NotFound(new { ok = false, message = "cart is empty" });
                }
                return Ok(new { ok = true, products = listOfCartedProduct });
            }
            catch (Exception ex)
            {
                return BadRequest(new { ok = false, ex.Message });
            }
            
        }
        [HttpGet("cart/{userId}")]
        public async Task<IActionResult> GetItemCount([FromRoute] int userId)
        {
            try
            {
                var productCount = await shoppingCartRepo.CartedProductCount(userId);

                return Ok(productCount);
            }
            catch (Exception ex)
            {
                return BadRequest(new { ok = false, ex.Message });
            }

        }
    }
}
