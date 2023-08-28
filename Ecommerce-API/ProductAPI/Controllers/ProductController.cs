using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductAPI.Model.DTO;
using ProductAPI.Repository;
using ProductAPI.Services.FileServices;

namespace ProductAPI.Controllers
{
    [Route("api/")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository productRepo;
        private readonly IFileService fileServices;
        public ProductController(IProductRepository productRepo, IFileService fileService)
        {
            this.productRepo = productRepo;
            this.fileServices = fileService;
        }

        [HttpGet("products")]
        public async Task<IActionResult> GetAllProduct([FromQuery] int page)
        {
            try
            {
                var listOfProduct = await productRepo.GetAllProductAsync(page);

                if (listOfProduct is null)
                {
                    return NotFound(new { message = "product not available" });
                }

                return Ok(new
                {
                    products = listOfProduct.Item1,
                    TotalNumberOfPage = listOfProduct.Item2
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("products")]
        public async Task<IActionResult> AddProduct([FromForm] ProductDTO product)
        {
            try
            {
                if(product is null)
                {
                    return BadRequest(new { message = "product not perfectly added" });
                }
                if (product.ImageFile is not null)
                {
                    var result = fileServices.SaveImage(product.ImageFile);
                    product.ImageUrl = result.Item2;
                }

                var isOk = await productRepo.CreateProductAsync(product);

                if (isOk)
                {
                    return Ok(product);
                }

                return BadRequest("item not added succesfully");
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("products/{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await productRepo.DeleteProduct(id);

            if (product is not null)
            {
                var result = fileServices.DeleteImage(product.ImageUrl);
                return Ok(new { Message = "deleted successfully" });
            }
            else
            {
                return BadRequest("internal server problem");
            }
        }
        [HttpGet("products/{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
           try
           {
                var product = await productRepo.GetProductByIdAsync(id);

                if (product is not null)
                {
                    return Ok(product);
                }

                return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpGet("products/catagory/{id}")]
        public async Task<IActionResult> GetProductsByCatagory([FromRoute] int id, [FromQuery] int page)
        {
           try
           {
                if (id is 0)
                {
                    return BadRequest();
                }

                var listOfProduct = await productRepo.GetProductsByCatagoryAsync(id, page);

                if (listOfProduct is null)
                {
                    return NotFound(new { message = "product not available" });
                }

                return Ok(new
                {
                    products = listOfProduct.Item1,
                    TotalNumberOfPage = listOfProduct.Item2
                });
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
