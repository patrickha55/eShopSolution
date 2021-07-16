using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using eShopSolution.Application.Catalog.Products;

namespace eShopSolution.BackendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IPublicProductService _publicProductService;
        public ProductController(IPublicProductService publicProductService)
        {
            _publicProductService = publicProductService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var products = await _publicProductService.GetAll();
            return Ok(products);
        }
    }
}
