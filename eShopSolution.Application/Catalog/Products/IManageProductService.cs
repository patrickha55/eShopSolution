using eShopSolution.Application.Catalog.Dtos;
using eShopSolution.Application.Catalog.Products.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace eShopSolution.Application.Catalog.Products
{
    public interface IManageProductService
    {
        Task<int> Create(ProductCreateRequest request);
        Task<int>  Create(ProductEditRequest request);
        Task<int>  Delete(int productId);

        List<ProductViewModel> GetAll();
        PageViewModel<ProductViewModel> GetAllPaging(string keyword, int pageIndex, int pageSize);
    }
}
