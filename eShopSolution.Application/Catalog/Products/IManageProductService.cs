using eShopSolution.ViewModels.Catalog.Products;
using eShopSolution.ViewModels.Common;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace eShopSolution.Application.Catalog.Products
{
    public interface IManageProductService
    {
        Task<ProductViewModel> GetById(int productId, string languageId);

        Task<PagedResult<ProductViewModel>> GetAllPaging(GetManageProductPagingRequest request);

        Task<List<ProductImageViewModel>> GetListImage(int productId);

        Task<int> Create(ProductCreateRequest request);

        Task AddViewcount(int productId);

        Task<int> AddImage(int productId, List<IFormFile> files);

        Task<int>  Update(ProductUpdateRequest request);

        Task<bool> UpdatePrice(int productId, decimal newPrice);

        Task<bool> UpdateStock(int productId, int stock);

        Task<int> UpdateImage(int imageId, string caption, bool isDefault);

        Task<int>  Delete(int productId);

        Task<int> RemoveImages(int imageId);
    }
}
