using Catalog.Core.CatalogSpecs;
using Catalog.Core.Entities;
using Catalog.Core.Repositories;
using Catalog.Infrastructure.Data;
using MongoDB.Driver;

namespace Catalog.Infrastructure.Repositories;

public class ProductRepository(ICatalogContext context) : IProductRepository
{
    public async Task<Product> CreateProduct(Product product)
    {
        await context.Products.InsertOneAsync(product);
        return product;
    }

    public async Task<bool> DeleteProduct(string id)
    {
        var result = await context.Products.DeleteOneAsync(x => x.Id == id);
        return result.IsAcknowledged && result.DeletedCount > 0;
    }

    public Task<bool> DeleteProduct(Product product)
    {
        return DeleteProduct(product.Id);
    }

    public async Task<Product> GetProductById(string id)
    {
        return await context.Products.Find(x => x.Id == id).FirstOrDefaultAsync();
    }

    public async Task<Pagination<Product>> GetProducts(CatalogSpecsParams specsParams)
    {

        var builder = Builders<Product>.Filter;//create filter
        var filters = builder.Empty;//empty
        if (!string.IsNullOrEmpty(specsParams.Search))
        {
            var searchFilter = builder.Where(x => x.Name.ToLower().Contains(specsParams.Search.ToLower()));
            filters &= searchFilter;
        }
        if (string.IsNullOrEmpty(specsParams.BrandId))
        {
            var brandFilter = builder.Eq(x => x.Brands.Id , specsParams.BrandId);
            filters &= brandFilter;
        }
        if (string.IsNullOrEmpty(specsParams.TypeId))
        {
            var typeFilter = builder.Eq(x=>x.Types.Id,specsParams.TypeId);
            filters &= typeFilter;
        }
        var totalItems = await context.Products.CountDocumentsAsync(filters);
        var sort = Builders<Product>.Sort.Ascending(x => x.Name);
        if (!string.IsNullOrEmpty(specsParams.Sort))
        {
            sort = specsParams.Sort switch
            {
                "priceAsc" => Builders<Product>.Sort.Ascending(x => x.Price),
                "priceDesc" => Builders<Product>.Sort.Descending(x => x.Price),
                _ => Builders<Product>.Sort.Ascending(x => x.Name),
            };
        }
        var data = await context.Products
            .Find(filters)
            .Sort(sort)
            .Skip(specsParams.PageSize * (specsParams.PageIndex - 1))
            .Limit(specsParams.PageSize)
            .ToListAsync();
        return new Pagination<Product>(specsParams.PageIndex, specsParams.PageSize, (int)totalItems,data);
    }

    public async Task<IEnumerable<Product>> GetProductsByBrand(string name)
    {
        return await context.Products.Find(x => x.Brands.Name == name).ToListAsync();
    }

    public async Task<IEnumerable<Product>> GetProductsByBrandId(string id)
    {
        return await context.Products.Find(x => x.Brands.Id == id).ToListAsync();
    }

    public async Task<IEnumerable<Product>> GetProductsByName(string name)
    {
        return await context.Products.Find(x => x.Brands.Name == name).ToListAsync();
    }

    public async Task<IEnumerable<Product>> GetProductsByType(string type)
    {
        return await context.Products.Find(x => x.Types.Name == type).ToListAsync();
    }

    public async Task<IEnumerable<Product>> GetProductsByTypeId(string id)
    {
        return await context.Products.Find(x => x.Types.Id == id).ToListAsync();
    }

    public async Task<bool> UpdateProduct(Product product)
    {
        var result = await context.Products.ReplaceOneAsync(x => x.Id == product.Id, product);
        return result.IsAcknowledged && result.ModifiedCount > 0;
    }
}
