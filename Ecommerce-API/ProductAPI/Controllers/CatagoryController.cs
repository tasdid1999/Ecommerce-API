using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductAPI.Model.Domain;
using ProductAPI.Repository;

namespace ProductAPI.Controllers
{
    [Route("api")]
    [ApiController]
    public class CatagoryController : ControllerBase
    {
        private readonly ICatagoryRepository _repo;

        public CatagoryController(ICatagoryRepository repo)
        {
            _repo = repo;
        }
        [HttpPost]
        [Route("catagories")]
        public async Task<IActionResult> AddCatagory([FromForm]Catagory catagory)
        {
            try
            {
                var isOk = await _repo.AddCatagoryAsync(catagory);

                if (isOk) return Ok(catagory);

                return BadRequest("item not added successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet]
        [Route("catagories")]
        public async Task<IActionResult> GetAllCatagories()
        {
            try
            {
                var catagories = await _repo.GetAllCatagoriesAsync();

                if (catagories.Count > 0) return Ok(catagories);

                return NotFound();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


    }
}
