using Catalog.Core.Entities;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace Catalog.Infrastructure.Data;

public class CatalogContext : ICatalogContext
{
    public IMongoCollection<Product> Products { get; }

    public IMongoCollection<ProductBrand> Brands { get; }

    public IMongoCollection<ProductType> Types {  get; }
    public CatalogContext(IConfiguration configuration)
    {
        var client = new MongoClient(configuration.GetValue<string>("DatabaseSettings:ConnectionString"));

        var database = client.GetDatabase(configuration.GetValue<string>("DatabaseSettings:DatabaseName"));
        //Get all of collections

        Products = database.GetCollection<Product>(configuration.GetValue<string>("DatabaseSettings:CollectionName"));

        Brands = database.GetCollection<ProductBrand>(configuration.GetValue<string>("DatabaseSettings:BrandCollection"));

        Types = database.GetCollection<ProductType>(configuration.GetValue<string>("DatabaseSettings:TypeCollection"));

        BrandSeedData.SeedData(Brands);
        TypeSeedData.SeedData(Types);
        ProductSeedData.SeedData(Products);
    }
}
