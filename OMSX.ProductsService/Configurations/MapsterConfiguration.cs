using Mapster;
using OMSX.ProductsService.Common.Protos;
using OMSX.ProductsService.Entities;
using OMSX.ProductsService.Protos;
using OMSX.Shared.Entities.Common;
using System.Reflection;
namespace OMSX.ProductsService.Configurations
{
    public class MapsterConfiguration : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.Default.IgnoreNullValues(true);
            config.NewConfig<Product, ProductResponse>();
            config.NewConfig<ProductRequest, Product>()
              ;
            config.NewConfig<ProductImage, ProductImageResponse>();
            config.NewConfig<ProductImageRequest, ProductImage>()
            ;
            config.NewConfig<ProductAttribute, ProductAttributeResponse>();
            config.NewConfig<ProductAttributeRequest, ProductAttribute>()
            ;
            config.NewConfig<ProductOptions, ProductOptionResponse>();
            config.NewConfig<ProductOptionRequest, ProductOptions>()
            ;
            config.NewConfig<Localization, LocalizationResponse>();
            config.NewConfig<LocalizationRequest, Localization>()
            ;
            config.NewConfig<Category, CategoryResponse>();
            config.NewConfig<CategoryRequest, Category>()
            ;
            config.NewConfig<Image, ImageResponse>();
            config.NewConfig<ImageRequest, Image>()
              ;

        }


    }
}