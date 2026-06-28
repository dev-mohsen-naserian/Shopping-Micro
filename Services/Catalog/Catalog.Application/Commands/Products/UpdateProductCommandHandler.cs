using AutoMapper;
using Catalog.Core.Entities;
using Catalog.Core.Repositories;
using MediatR;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Catalog.Application.Commands.Products;

public class UpdateProductCommand:IRequest<bool>
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Summery { get; set; }
    public string Description { get; set; }
    public string ImageFile { get; set; }
    public decimal Price { get; set; }
    //Relations
    public ProductBrand Brands { get; set; }
    public ProductType Types { get; set; }
}
public class UpdateProductCommandHandler(IProductRepository productRepository ,IMapper mapper) : IRequestHandler<UpdateProductCommand, bool>
{
    public async Task<bool> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
       var entity = mapper.Map<Product>(request);
       return await productRepository.UpdateProduct(entity);
    }
}
