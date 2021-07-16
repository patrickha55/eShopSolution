using eShopSolution.Data.EF;
using eShopSolution.Data.Entities;
using eShopSolution.Utilities.Exceptions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using eShopSolution.ViewModels.Catalog.Products;
using eShopSolution.ViewModels.Common;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Http;
using System.IO;
using eShopSolution.Application.Common;

namespace eShopSolution.Application.Catalog.Products
{
    public class ManageProductService : IManageProductService
    {
        private readonly EShopDbContext _context;
        private readonly IStorageService _storageService;

        public ManageProductService(EShopDbContext context, IStorageService storageService)
        {
            _context = context;
            _storageService = storageService;
        }
        public async Task<int> Create(ProductCreateRequest request)
        {
            var product = new Product()
            {
                Price = request.Price,
                OriginalPrice = request.OriginalPrice,
                Stock = request.Stock,
                ViewCount = 0,
                DateCreated = DateTime.Now,
                ProductTranslations = new List<ProductTranslation>()
                {
                    new ProductTranslation()
                    {
                        Name = request.Name,
                        Description = request.Description,
                        Details = request.Details,
                        SeoDescription = request.SeoDescription,
                        SeoTitle = request.SeoTitle,
                        SeoAlias = request.SeoAlias,
                        LanguageId = request.LanguageId
                    }
                }
            };

            if (request.ThumbnailImage != null)
            {
                product.ProductImages = new List<ProductImage>()
                {
                    new ProductImage()
                    {
                        Caption = $"Thumbnail image: {request.Name}",
                        DateCreated = DateTime.Now,
                        FileSize = request.ThumbnailImage.Length,
                        ImagePath = await this.SaveFile(request.ThumbnailImage),
                        IsDefault = true,
                        SortOrder = 1
                    }
                };
            }

            _context.Products.Add(product);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> Update(ProductUpdateRequest request)
        {
            var product = await _context.Products.FindAsync(request.Id);
            var productTranlations = await _context.ProductTranslations.FirstOrDefaultAsync(
                x => x.ProductId == request.Id && x.LanguageId == request.LanguageId);

            if (product is null || productTranlations is null) throw new EShopException("Product with provided id does not exist!");

            productTranlations.Name = request.Name;
            productTranlations.Description = request.Description;
            productTranlations.Details = request.Details;
            productTranlations.SeoDescription = request.SeoDescription;
            productTranlations.SeoTitle = request.SeoTitle;
            productTranlations.SeoAlias = request.SeoAlias;

            if (request.ThumbnailImage != null)
            {
                var thumbnailImage = await _context.ProductImages.FirstOrDefaultAsync(x => x.IsDefault == true && x.ProductId == request.Id);

                if (thumbnailImage != null)
                {
                    thumbnailImage.FileSize = request.ThumbnailImage.Length;
                    thumbnailImage.ImagePath = await this.SaveFile(request.ThumbnailImage);
                    _context.ProductImages.Update(thumbnailImage);
                }
            }

            return await _context.SaveChangesAsync();
        }

        public async Task<int> Delete(int productId)
        {
            var product = await _context.Products.FindAsync(productId);
            var images = _context.ProductImages.Where(x => x.ProductId == productId);

            if (product is null || images is null) throw new EShopException("Product id does not exists!");

            foreach (var image in images)
            {
                await _storageService.DeleteFileAsync(image.ImagePath);
            }

            _context.Products.Remove(product);

            return await _context.SaveChangesAsync();
        }

        public async Task AddViewcount(int productId)
        {
            var product = await _context.Products.FindAsync(productId);

            product.ViewCount += 1;

            await _context.SaveChangesAsync();
        }

        public async Task<bool> UpdatePrice(int productId, decimal newPrice)
        {
            var product = await _context.Products.FindAsync(productId);

            if (product is null) throw new EShopException("The product with provided id does not exist!");

            product.Price = newPrice;

            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateStock(int productId, int stock)
        {
            var product = await _context.Products.FindAsync(productId);

            if (product is null) throw new EShopException("The product with provided id does not exist!");

            product.Stock += stock;

            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<PagedResult<ProductViewModel>> GetAllPaging(GetManageProductPagingRequest request)
        {
            var query = from p in _context.Products
                        join pt in _context.ProductTranslations on p.Id equals pt.ProductId
                        join pic in _context.ProductInCategories on pt.ProductId equals pic.ProductId
                        join c in _context.Categories on pic.CategoryId equals c.Id
                        select new { p, pt, pic };

            //filter
            if (!string.IsNullOrEmpty(request.Keyword))
            {
                query = query.Where(x => x.pt.Name.Contains(request.Keyword));
            }

            if (request.CategoryIds.Count > 0)
            {
                query = query.Where(p => request.CategoryIds.Contains(p.pic.CategoryId));
            }

            //paging
            int totalRow = await query.CountAsync();

            var data = await query.Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(x => new ProductViewModel()
                {
                    Id = x.p.Id,
                    Price = x.p.Price,
                    OriginalPrice = x.p.OriginalPrice,
                    Stock = x.p.Stock,
                    ViewCount = x.p.ViewCount,
                    DateCreated = x.p.DateCreated,
                    Name = x.pt.Name,
                    Description = x.pt.Description,
                    Details = x.pt.Details,
                    SeoDescription = x.pt.SeoDescription,
                    SeoTitle = x.pt.SeoTitle,
                    SeoAlias = x.pt.SeoAlias,
                    LanguageId = x.pt.LanguageId
                }).ToListAsync();

            //Select and projection
            var pagedResult = new PagedResult<ProductViewModel>()
            {
                TotalRecord = totalRow,
                Items = data
            };

            return pagedResult;
        }

        private async Task<string> SaveFile(IFormFile file)
        {
            var originalFileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(originalFileName)}";
            await _storageService.SaveFileAsync(file.OpenReadStream(), fileName);
            return fileName;
        }

        public async Task<int> AddImage(int productId, List<IFormFile> files)
        {
            var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == productId);

            if (product is null) throw new EShopException("Product with provided id does not exists!");

            foreach (var file in files)
            {
                product.ProductImages = new List<ProductImage>()
                {
                    new ProductImage()
                    {
                        Caption = $"Thumbnail image: {file.FileName.Trim()}",
                        DateCreated = DateTime.Now,
                        FileSize = file.Length,
                        ImagePath = await this.SaveFile(file),
                        IsDefault = true,
                        SortOrder = 1
                    }
                };
            }

            return await _context.SaveChangesAsync();
        }

        public async Task<int> UpdateImage(int imageId, string caption, bool isDefault)
        {
            var image = await _context.ProductImages.FirstOrDefaultAsync(i => i.Id == imageId);

            if (image is null) throw new EShopException("Image does not exists!");

            image.Caption = caption;
            image.IsDefault = isDefault;

            return await _context.SaveChangesAsync();
        }

        public async Task<int> RemoveImages(int imageId)
        {
            var image = await _context.ProductImages.FirstOrDefaultAsync(i => i.Id == imageId);
            //var product = await _context.Products.FirstOrDefaultAsync();

            if (image is null) throw new EShopException("Image does not exists!");

            _context.ProductImages.Remove(image);

            return await _context.SaveChangesAsync();
        }

        public async Task<List<ProductImageViewModel>> GetListImage(int productId)
        {
            var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == productId);

            if (product is null) throw new EShopException("Product with provided id does not exists!");

            List<ProductImageViewModel> imageViewModels = new List<ProductImageViewModel>();

            foreach (var image in product.ProductImages)
            {
                imageViewModels.Add(new ProductImageViewModel()
                {
                    Id = image.Id,
                    FileSize = image.FileSize,
                    ImagePath = image.ImagePath,
                    IsDefault = image.IsDefault
                });
            };

            return imageViewModels;
        }
    }
}
