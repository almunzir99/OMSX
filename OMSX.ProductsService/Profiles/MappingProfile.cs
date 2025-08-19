using OMSX.ProductsService.Entities;
using OMSX.ProductsService.Protos;
using OMSX.Shared.Entities.Common;
namespace OMSX.ProductsService.Profiles
{
    public class MappingProfile : AutoMapper.Profile
    {
        public MappingProfile()
        {

            CreateMap<Product, ProductResponse>();
            CreateMap<ProductRequest, Product>();

            CreateMap<ProductImage, ProductImageResponse>();
            CreateMap<ProductImageRequest, ProductImage>();
            CreateMap<ProductAttribute, ProductAttributeResponse>();
            CreateMap<ProductAttributeRequest, ProductAttribute>();
            CreateMap<ProductOptions, ProductOptionResponse>();
            CreateMap<ProductOptionRequest, ProductOptions>();
            CreateMap<Localization, LocalizationResponse>();
            CreateMap<LocalizationRequest, LocalizationResponse>();
            CreateMap<Category, CategoryResponse>();
            CreateMap<CategoryRequest, Category>();
            CreateMap<Image, ImageResponse>();
            CreateMap<ImageRequest, Image>();



        }
    }
}
