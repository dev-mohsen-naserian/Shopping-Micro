using AutoMapper;
using Catalog.Application.Commands.Products;
using Catalog.Application.Responses;
using Catalog.Core.CatalogSpecs;
using Catalog.Core.Entities;

namespace Catalog.Application.Mapper;

public class ProfileMapper : Profile
{
    public ProfileMapper()
    {
        CreateMap<ProductBrand, BrandResponse>().ReverseMap();
        CreateMap<ProductType, TypeResponse>().ReverseMap();
        CreateMap<Product, ProductResponse>().ReverseMap();
        CreateMap<Product, CreateProductCommand>().ReverseMap();
        CreateMap<Product,UpdateProductCommand>().ReverseMap();
        CreateMap<Pagination<Product>,Pagination<ProductResponse>>().ReverseMap();
    }
}
