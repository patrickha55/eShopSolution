using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using eShopSolution.Application.Catalog.Products;
using eShopSolution.ViewModels.Catalog.Products;

namespace eShopSolution.BackendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IPublicProductService _publicProductService;
        private readonly IManageProductService _manageProductService;
        public ProductController(IPublicProductService publicProductService, IManageProductService manageProductService)
        {
            _publicProductService = publicProductService;
            _manageProductService = manageProductService;
        }

        //public
        //http://localhost:port/product
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var products = await _publicProductService.GetAll();
            return Ok(products);
        }

        //http://localhost:port/product/paging-request
        [HttpGet("{request}")]
        public async Task<IActionResult> Get([FromQuery] GetPublicProductPagingRequest request)
        {
            var product = await _publicProductService.GetAllByCategoryId(request);
            return Ok(product);
        }


        //manage
        //http://localhost:port/product/product-id/language-id
        [HttpGet("{product-id}")]
        public async Task<IActionResult> GetById([FromQuery]int productId, string languageId)
        {
            var products = await _manageProductService.GetById(productId, languageId);

            if (products is null) return NotFound("Cannot Find Product");

            return Ok(products);
        }

        [HttpGet("Product/{request}")]
        public async Task<IActionResult> GetAllPaging([FromQuery]GetManageProductPagingRequest request)
        {
            var product = await _manageProductService.GetAllPaging(request);
            return Ok(product);
        }

        /*[HttpPost]
        public async Task<IActionResult> Create([FromBody] ProductCreateRequest request)
        {
            var result = await _manageProductService.Create(request);

            if (result == 0) return BadRequest();

            return CreatedAtAction(nameof(GetById), new { productId = });
        }*/


        [HttpPut]
        public async Task<IActionResult> Update([FromBody]ProductUpdateRequest request)
        {
            var result = await _manageProductService.Update(request);

            if (result == 0) return BadRequest();

            return Ok();
        }

        [HttpPut("price/{id}/{newPrice}")]
        public async Task<IActionResult> UpdatePrice([FromQuery]int id, decimal newPrice)
        {
            var result = await _manageProductService.UpdatePrice(id, newPrice);

            if (result is false) return BadRequest();

            return Ok();
        }

        [HttpPut("stock/{id}/{stock}")]
        public async Task<IActionResult> UpdateStock([FromQuery]int id, int stock)
        {
            var result = await _manageProductService.UpdateStock(id, stock);

            if (result is false) return BadRequest();

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _manageProductService.Delete(id);

            if (result == 0) return BadRequest();

            return Ok();
        }
    }
}
