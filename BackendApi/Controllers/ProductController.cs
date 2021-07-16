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

        //http://localhost:port/product
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var products = await _publicProductService.GetAll();
            return Ok(products);
        }

        //http://localhost:port/product/paging-request
        [HttpGet("{paging-request}")]
        public async Task<IActionResult> Get([FromQuery] GetPublicProductPagingRequest request)
        {
            var product = await _publicProductService.GetAllByCategoryId(request);
            return Ok(product);
        }

        //http://localhost:port/product/product-id/language-id
        [HttpGet("{product-id}")]
        public async Task<IActionResult> GetById(int productId, string languageId)
        {
            var products = await _manageProductService.GetById(productId, languageId);

            if (products is null) return NotFound("Cannot Find Product");

            return Ok(products);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ProductCreateRequest request)
        {
            var result = await _manageProductService.Create(request);

            if (request is null) return BadRequest();

            return CreatedAtAction(nameof(GetById), );
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody])
        {

        }
    }
}
