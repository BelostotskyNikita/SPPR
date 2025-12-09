using WEB_353502_Belostotsky.API.Data;
using WEB_353502_Belostotsky.Domain.Entities;
using WEB_353502_Belostotsky.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace WEB_353502_Belostotsky.API.Services.ProductService
{
    public class ProductService : IProductService
    {
        private readonly AppDbContext _context;
        private readonly int _maxPageSize = 20;
        public ProductService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ResponseData<ClothingItem>> GetProductByIdAsync(int id)
        {
            var product = await _context.ClothingItems.FindAsync(id);
            if (product == null)
            {
                return ResponseData<ClothingItem>.Error("Product not found");
            }
            return ResponseData<ClothingItem>.Success(product);
        }

        public async Task UpdateProductAsync(int id, ClothingItem product)
        {
            var existingProduct = await _context.ClothingItems.FindAsync(id);
            if (existingProduct == null)
            {
                throw new Exception("Product not found");
            }
            existingProduct.Name = product.Name;
            existingProduct.Description = product.Description;
            existingProduct.Price = product.Price;
            existingProduct.CategoryId = product.CategoryId;
            await _context.SaveChangesAsync();
        }

        public async Task<ResponseData<object>> DeleteProductAsync(int id)
        {
            var product = await _context.ClothingItems.FindAsync(id);
            if (product == null)
            {
                return ResponseData<object>.Error("Product not found");
            }

            _context.ClothingItems.Remove(product);
            await _context.SaveChangesAsync();

            return ResponseData<object>.Success(null);  // Успешное удаление
        }


        public async Task<ResponseData<ClothingItem>> CreateProductAsync(ClothingItem product)
        {
            _context.ClothingItems.Add(product);
            await _context.SaveChangesAsync();
            return ResponseData<ClothingItem>.Success(product);
        }

        public async Task<ResponseData<string>> SaveImageAsync(int id, IFormFile formFile)
        {
            // Здесь должен быть код для сохранения изображения и возвращения URL
            return ResponseData<string>.Success("Image saved successfully");
        }

        public async Task<ResponseData<ListModel<ClothingItem>>> GetProductListAsync(
        string? categoryNormalizedName,
        int pageNo = 1,
        int pageSize = 3)
        {
            if (pageSize > _maxPageSize)
                pageSize = _maxPageSize;
            var query = _context.ClothingItems.AsQueryable();
            var dataList = new ListModel<ClothingItem>();
            query = query
            .Where(d => categoryNormalizedName == null
            ||
            d.Category.NormalizedName.Equals(categoryNormalizedName));
            // количество элементов в списке
            var count = await query.CountAsync(); //.Count();
            if (count == 0)
            {
                return ResponseData<ListModel<ClothingItem>>.Success(dataList);
            }
            // количество страниц
            int totalPages = (int)Math.Ceiling(count / (double)pageSize);
            if (pageNo > totalPages)
                return ResponseData<ListModel<ClothingItem>>.Error("No such page");
            dataList.Items = await query
            .OrderBy(d => d.Id)
            .Skip((pageNo - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
            dataList.CurrentPage = pageNo;
            dataList.TotalPages = totalPages;
            return ResponseData<ListModel<ClothingItem>>.Success(dataList);
        }
    }


}
