using Mapster;
using Microsoft.AspNetCore.Mvc;
using Omsx.Vendors;
using OMSX.Shared.Entities.Common;
using OMSX.VendorsService.Common.Protos;
using OMSX.VendorsService.Entities;
using OMSX.VendorsService.Protos;
using static System.Net.Mime.MediaTypeNames;
using FileInfo = OMSX.VendorsService.Entities.FileInfo;

namespace OMSX.VendorsService.Configuration
{
    public class MapsterConfiguration : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.Default.IgnoreNullValues(true);
            config.NewConfig<Vendor, VendorResponse>();
            config.NewConfig<VendorRequest, Vendor>();
            config.NewConfig<Localization, LocalizationResponse>();
            config.NewConfig<LocalizationRequest, Localization>();
            config.NewConfig<VendorCategory, VendorCategoryResponse>();
            config.NewConfig<VendorCategoryRequest, VendorCategory>();
            config.NewConfig<FileInfo, FileInfoResponse>();
            config.NewConfig<FileInfoRequest, FileInfo>();
            config.NewConfig<BusinessAddress, BusinessAddressResponse>();
            config.NewConfig<BusinessAddressRequest, BusinessAddress>();
        }
    }
}
