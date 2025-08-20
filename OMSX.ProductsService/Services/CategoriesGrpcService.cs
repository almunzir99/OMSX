using Grpc.Core;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;
using OMSX.ProductsService.Common.Protos;
using OMSX.ProductsService.Interfaces;
using OMSX.ProductsService.Protos;
using OMSX.Shared.Extensions;
using static OMSX.ProductsService.Protos.CategoriesService;

namespace OMSX.ProductsService.Services
{
    public class CategoriesGrpcService : CategoriesServiceBase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        public CategoriesGrpcService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        public override async Task<CategoryResponse> GetCategoryById(CategoryIdRequest request, ServerCallContext context)
        {
            var guid = request.Id.ToGuid();
            var category = await unitOfWork.CategoriesRepository.FindAsync(x => x.Id == guid);
            if (category == null)
            {
                throw new RpcException(new Status(StatusCode.NotFound, "Category not found"));
            }
            return mapper.Map<CategoryResponse>(category);
        }
        public override async Task<PagedCategoryResponse> GetCategories(PagedRequest request, ServerCallContext context)
        {
            var query = unitOfWork.CategoriesRepository.Query();
            if (!string.IsNullOrEmpty(request.SearchTerm))
            {
                query = query.Where(c => c.CategoryName.ArContent.Contains(request.SearchTerm)
                 || c.CategoryName.EnContent.Contains(request.SearchTerm)

                );
            }
            var totalCount = await query.CountAsync();
            var categories = await query
                .OrderByDescending(c => c.CreatedAt)
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync();
            var categoryResponses = mapper.Map<List<CategoryResponse>>(categories);
            return new PagedCategoryResponse
            {
                Data = { categoryResponses },
                TotalCount = totalCount,
                PageNumber = request.PageNumber,
                PageSize = request.PageSize
            };
        }
        public override async Task<CategoryResponse> CreateCategory(CategoryRequest request, ServerCallContext context)
        {
            var category = mapper.Map<Entities.Category>(request);
            var created = await unitOfWork.CategoriesRepository.CreateAsync(category);
            await unitOfWork.Complete();
            return mapper.Map<CategoryResponse>(created);
        }
        public override async Task<CategoryResponse> UpdateCategory(CategoryRequest request, ServerCallContext context)
        {
            var guid = request.Id.ToGuid();
            var category = await unitOfWork.CategoriesRepository.FindAsync(x => x.Id == guid);
            if (category == null)
            {
                throw new RpcException(new Status(StatusCode.NotFound, "Category not found"));
            }
            mapper.Map(request, category);
            await unitOfWork.CategoriesRepository.UpdateAsync(category);
            await unitOfWork.Complete();
            return mapper.Map<CategoryResponse>(category);
        }
    }
}
