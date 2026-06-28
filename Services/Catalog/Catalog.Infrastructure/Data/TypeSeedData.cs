using Catalog.Core.Entities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace Catalog.Infrastructure.Data;

public static class TypeSeedData
{
    public static void SeedData(IMongoCollection<ProductType> collection)
    {
        var isExistCollection = collection.Find(x => true).Any();
        if (isExistCollection)
            return;
        var pathJson = Path.Combine(AppContext.BaseDirectory, "Data", "SeedData", "types.json");
        if (File.Exists(pathJson))
        {
            throw new Exception($"the seed data of the type.json not find at path : {pathJson}");
        }
        var dataText = File.ReadAllText(pathJson);
        var brands = JsonSerializer.Deserialize<List<ProductType>>(dataText);
        if (brands != null)
            collection.InsertMany(brands);
    }
}
