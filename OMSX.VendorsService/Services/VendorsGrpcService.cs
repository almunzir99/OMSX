using Grpc.Core;
using MapsterMapper;
using Omsx.Vendors;
using OMSX.VendorsService.Interfaces;
using OMSX.Shared.Extensions;
using static Omsx.Vendors.VendorsService;
using OMSX.VendorsService.Common.Protos;
using Microsoft.EntityFrameworkCore;

namespace OMSX.VendorsService.Services
{
    public class VendorsGrpcService : VendorsServiceBase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public VendorsGrpcService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        public override async Task<VendorResponse> CreateVendor(VendorRequest request, ServerCallContext context)
        {
            var vendor = mapper.Map<Entities.Vendor>(request);
            await unitOfWork.VendorsRepository.CreateAsync(vendor);
            await unitOfWork.Complete();
            var response = mapper.Map<VendorResponse>(vendor);
            return response;
        }
        public override async Task<VendorResponse> UpdateVendor(VendorRequest request, ServerCallContext context)
        {
            var vendor = await unitOfWork.VendorsRepository.FindAsync(x => x.Id == request.Id.ToGuid());
            if (vendor == null)
            {
                throw new RpcException(new Status(StatusCode.NotFound, $"Vendor with ID {request.Id} not found."));
            }
            mapper.Map(request, vendor);
            await unitOfWork.VendorsRepository.UpdateAsync(vendor);
            await unitOfWork.Complete();
            var response = mapper.Map<VendorResponse>(vendor);
            return response;
        }
        public override Task<VendorResponse> GetVendorById(IdRequest request, ServerCallContext context)
        {
            var guid = request.Id.ToGuid();
            var vendor = unitOfWork.VendorsRepository.FindAsync(x => x.Id == guid, x => x.BusinessName, x => x.Description, x => x.BusinessAddress, x => x.Logo, x => x.VendorCategory);
            if (vendor == null)
            {
                throw new RpcException(new Status(StatusCode.NotFound, $"Vendor with ID {request.Id} not found."));
            }
            var response = mapper.Map<VendorResponse>(vendor);
            return Task.FromResult(response);
        }
        public override Task<PagedVendorsResponse> GetVendors(PagedRequest request, ServerCallContext context)
        {
            var query = unitOfWork.VendorsRepository.Query()
                .Include(x => x.BusinessName)
                .Include(x => x.Description)
                .Include(x => x.BusinessAddress)
                .Include(x => x.Logo)
                .Include(x => x.VendorCategory).AsQueryable();
            if (!string.IsNullOrEmpty(request.SearchTerm))
            {
                query = query.Where(c => c.BusinessName!.ArContent.Contains(request.SearchTerm)
                 || c.BusinessName.EnContent.Contains(request.SearchTerm)
                 || c.LegalName.Contains(request.SearchTerm)
                 || c.VendorCategory!.CategoryName!.ArContent.Contains(request.SearchTerm)
                 || c.VendorCategory.CategoryName.EnContent.Contains(request.SearchTerm)
                );
            }
            if (request.PageSize == 0) request.PageSize = 10;
            if (request.PageNumber == 0) request.PageNumber = 1;
            var totalCount = query.Count();
            var vendors = query
                .OrderByDescending(c => c.CreatedAt)
                .Skip((request.PageNumber - 1) * request.PageSize)
            .Take(request.PageSize)
                .ToList();
            var vendorResponses = mapper.Map<List<VendorResponse>>(vendors);
            var response = new PagedVendorsResponse
            {
                Data = { vendorResponses },
                TotalCount = totalCount,
                PageNumber = request.PageNumber,
                PageSize = request.PageSize
            };
            return Task.FromResult(response);
        }
        public override async Task<EmptyResponse> DeleteVendor(IdRequest request, ServerCallContext context)
        {
            var guid = request.Id.ToGuid();
            await unitOfWork.VendorsRepository.DeleteAsync(guid);
            await unitOfWork.Complete();
            return new EmptyResponse();
        }
        public override Task<VendorResponse> GetVendorsByVendorId(VendorRequest request, ServerCallContext context)
        {
            var vendor = unitOfWork.VendorsRepository.FindAsync(x => x.Id == request.Id.ToGuid(), x => x.BusinessName, x => x.Description, x => x.BusinessAddress, x => x.Logo, x => x.VendorCategory);
            if (vendor == null)
            {
                throw new RpcException(new Status(StatusCode.NotFound, $"Vendor with ID {request.Id} not found."));
            }
            var response = mapper.Map<VendorResponse>(vendor);
            return Task.FromResult(response);
        }
        public override Task<VendorResponse> GetVendorsByCategoryId(VendorRequest request, ServerCallContext context)
        {
            var vendor = unitOfWork.VendorsRepository.FindAsync(x => x.VendorCategoryId == request.VendorCategoryId.ToGuid(), x => x.BusinessName, x => x.Description, x => x.BusinessAddress, x => x.Logo, x => x.VendorCategory);
            if (vendor == null)
            {
                throw new RpcException(new Status(StatusCode.NotFound, $"Vendor with Category ID {request.VendorCategoryId} not found."));
            }
            var response = mapper.Map<VendorResponse>(vendor);
            return Task.FromResult(response);
        }
    }

}
