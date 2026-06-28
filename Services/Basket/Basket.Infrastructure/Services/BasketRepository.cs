using Basket.Core.Entities;
using Basket.Core.Repositories;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Basket.Infrastructure.Services;

public class BasketRepository(IDistributedCache redis) : IBasketRepository
{
    public async Task DeleteBasket(string userName)
    {

        await redis.RemoveAsync(userName);
    }

    public async Task<ShoppingCart?> GetBasket(string userName)
    {
        var basket = await redis.GetStringAsync(userName);

        return string.IsNullOrEmpty(basket) ? null : JsonConvert.DeserializeObject<ShoppingCart>(basket);
    }

    public async Task<ShoppingCart?> UpdateBasker(ShoppingCart shoppingCart)
    {
        await redis.SetStringAsync(shoppingCart.UserName,JsonConvert.SerializeObject(shoppingCart));

        return await GetBasket(shoppingCart.UserName);
    }
}
