using Catalog.Core.Entities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace Catalog.Infrastructure.Data;

public interface ICatalogContext
{
    IMongoCollection<Product> Products { get; }
    IMongoCollection<ProductBrand> Brands { get; }
    IMongoCollection<ProductType> Types { get; }
}
