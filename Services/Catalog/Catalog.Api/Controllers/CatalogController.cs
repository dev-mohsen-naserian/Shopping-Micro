using Catalog.Application.Commands.Products;
using Catalog.Application.Queries.Brands;
using Catalog.Application.Queries.Products;
using Catalog.Application.Queries.Types;
using Catalog.Application.Responses;
using Catalog.Core.CatalogSpecs;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.Api.Controllers;
public class CatalogController(IMediator mediator) : ApiController
{
    [HttpGet("{id}")]
    public async Task<ActionResult<ProductResponse>> GetProductById([FromRoute] string id, CancellationToken token)
    {
        return Ok(await mediator.Send(new GetProductByIdQuery(id), token));
    }
    [HttpGet("{name}")]
    public async Task<ActionResult<IEnumerable<ProductResponse>>> GetProductsByName(string name, CancellationToken token)
    {
        return Ok(await mediator.Send(new GetProductsByNameQuery(name), token));
    }

    [HttpGet]
    public async Task<ActionResult<Pagination<ProductResponse>>> GetAllProducts([FromQuery]GetAllProductsQuery request, CancellationToken token)
    {
        var result = await mediator.Send(request, token);
        return Ok(result);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProductResponse>>> GetAllBrands(CancellationToken token)
    {
        return Ok(await mediator.Send(new GetAllProductBrandsQuery(), token));
    }
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProductResponse>>> GetAllTypes(CancellationToken token)
    {
        return Ok(await mediator.Send(new GetAllProductsTypeQuery(), token));
    }

    [HttpGet("{brand}")]
    public async Task<ActionResult<IEnumerable<ProductResponse>>> GetProductsByBrandName([FromRoute]string brand,CancellationToken token)
    {
        return Ok(await mediator.Send(new GetProductsByBrandQuery(brand)));
    }
    [HttpGet("{brand}")]
    public async Task<ActionResult<IEnumerable<ProductResponse>>> GetProductsByTypeName([FromRoute]string brand, CancellationToken token)
    {
        return Ok(await mediator.Send(new GetProductsByTypeQuery(brand)));
    }
    [HttpPost]
    public async Task<ActionResult<ProductResponse>> CreateProduct([FromBody] CreateProductCommand create,CancellationToken token)
    {
        return Ok(await mediator.Send(create,token));
    }
    [HttpPut]
    public async Task<ActionResult<bool>> UpdateProduct([FromBody] UpdateProductCommand update,CancellationToken token)
    {
        return Ok(await mediator.Send(update, token));
    }
    [HttpDelete("{id}")]
    public async Task<ActionResult<bool>> DeleteProduct([FromRoute] string id,CancellationToken token)
    {
        return Ok(await mediator.Send(new DeleteProductCommand(id),token));
    }

}
