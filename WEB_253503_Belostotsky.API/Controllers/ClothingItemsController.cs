using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WEB_253503_Belostotsky.API.Services.ProductService;
using WEB_2535503_Belostotsky.Domain.Entities;
using WEB_2535503_Belostotsky.Domain.Models;

namespace WEB_253503_Belostotsky.API.Controllers
{
    [Route("catalog")]
    [ApiController]
    public class ClothingItemsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ClothingItemsController(IProductService productService)
        {
            _productService = productService;
        }

        // GET: api/ClothingItems/outerwear?pageno=2
        [HttpGet("{category}")]
        public async Task<ActionResult<ResponseData<List<ClothingItem>>>> GetClothingItems(string category, int pageNo = 1, int pageSize = 3)
        {
            var response = await _productService.GetProductListAsync(category, pageNo, pageSize);
            return Ok(response);
        }

        // GET: api/ClothingItems/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<ResponseData<ClothingItem>>> GetClothingItem(int id)
        {
            var response = await _productService.GetProductByIdAsync(id);
            if (!response.Successfull)
            {
                return NotFound(response.ErrorMessage);
            }
            return Ok(response);
        }

        // POST: api/ClothingItems
        [HttpPost]
        public async Task<ActionResult<ResponseData<ClothingItem>>> PostClothingItem(ClothingItem clothingItem)
        {
            var response = await _productService.CreateProductAsync(clothingItem);
            return CreatedAtAction(nameof(GetClothingItem), new { id = response.Data.Id }, response);
        }

        // DELETE: api/ClothingItems/5
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteClothingItem(int id)
        {
            var response = await _productService.DeleteProductAsync(id);
            if (!response.Successfull)
            {
                return NotFound(response.ErrorMessage);
            }

            return NoContent();
        }
    }
}
