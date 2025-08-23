using Grpc.Core;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;
using OMSX.Shared.Extensions;
using OMSX.Shared.Interfaces;
using OMSX.VendorsService.Common.Protos;
using OMSX.VendorsService.Entities;
using OMSX.VendorsService.Implementations;
using OMSX.VendorsService.Interfaces;
using OMSX.VendorsService.Protos;
using static OMSX.VendorsService.Protos.VendorCategoriesService;

namespace OMSX.VendorsService.Services
{
    public class VendorCategoriesGrpcService : VendorCategoriesServiceBase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public VendorCategoriesGrpcService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }


        public override async Task<PagedVendorCategoryResponse> GetCategories(PagedRequest request, ServerCallContext context)
        {
            var query = unitOfWork.VendorCategoriesRepository.Query().Include(x => x.Description).Include(x => x.CategoryName).Include(x => x.Image).AsQueryable();
            if (!string.IsNullOrEmpty(request.SearchTerm))
            {
                query = query.Where(c => c.CategoryName!.ArContent.Contains(request.SearchTerm)
                 || c.CategoryName.EnContent.Contains(request.SearchTerm)

                );
            }
            if (request.PageSize == 0) request.PageSize = 10;
            if (request.PageNumber == 0) request.PageNumber = 1;

            var totalCount = await query.CountAsync();
            var categories = await query
                .OrderByDescending(c => c.CreatedAt)
                .Skip((request.PageNumber - 1) * request.PageSize)
            .Take(request.PageSize)
                .ToListAsync();
            var categoryResponses = mapper.Map<List<VendorCategoryResponse>>(categories);
            return new PagedVendorCategoryResponse
            {
                Data = { categoryResponses },
                TotalCount = totalCount,
                PageNumber = request.PageNumber,
                PageSize = request.PageSize
            };
        }
        public override Task<VendorCategoryResponse> GetCategoryById(IdRequest request, ServerCallContext context)
        {
            var guid = request.Id.ToGuid();
            var category = unitOfWork.VendorCategoriesRepository.FindAsync(x => x.Id == guid, x => x.CategoryName);
            if (category == null)
            {
                throw new RpcException(new Status(StatusCode.NotFound, "Vendor Category not found"));
            }
            return Task.FromResult(mapper.Map<VendorCategoryResponse>(category));
        }
        public override async Task<VendorCategoryResponse> CreateCategory(VendorCategoryRequest request, ServerCallContext context)
        {
            var category = mapper.Map<VendorCategory>(request);
            var created = await unitOfWork.VendorCategoriesRepository.CreateAsync(category);
            await unitOfWork.Complete();
            return mapper.Map<VendorCategoryResponse>(created);
        }
        public override async Task<VendorCategoryResponse> UpdateCategory(VendorCategoryRequest request, ServerCallContext context)
        {
            var guid = request.Id.ToGuid();
            var category = await unitOfWork.VendorCategoriesRepository.FindAsync(x => x.Id == guid);
            if (category == null)
            {
                throw new RpcException(new Status(StatusCode.NotFound, "Vendor Category not found"));
            }
            mapper.Map(request, category);
            await unitOfWork.VendorCategoriesRepository.UpdateAsync(category);
            await unitOfWork.Complete();
            return mapper.Map<VendorCategoryResponse>(category);
        }
        public override async Task<EmptyResponse> DeleteCategory(IdRequest request, ServerCallContext context)
        {
            var guid = request.Id.ToGuid();
            await unitOfWork.VendorCategoriesRepository.DeleteAsync(guid);
            await unitOfWork.Complete();
            return new EmptyResponse();
        }
    }
}
