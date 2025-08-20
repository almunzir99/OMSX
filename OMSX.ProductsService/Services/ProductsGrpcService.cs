using Grpc.Core;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;
using OMSX.ProductsService.Common.Protos;
using OMSX.ProductsService.Interfaces;
using OMSX.ProductsService.Protos;
using OMSX.Shared.Extensions;
using static OMSX.ProductsService.Protos.ProductsService;

namespace OMSX.ProductsService.Services
{
    public class ProductsGrpcService : ProductsServiceBase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        public ProductsGrpcService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public override async Task<PagedProductResponse> GetProducts(PagedRequest request, ServerCallContext context)
        {
            var query = unitOfWork.ProductsRepository.Query();
            if (!string.IsNullOrEmpty(request.SearchTerm))
            {
                query = query.Where(p => p.ProductName.ArContent.Contains(request.SearchTerm)
                || p.Description.ArContent.Contains(request.SearchTerm) ||
                p.ProductName.EnContent.Contains(request.SearchTerm)
                || p.Description.EnContent.Contains(request.SearchTerm)
                );
            }
            var totalCount = await query.CountAsync();
            var products = await query
                .Include(p => p.Category)
                .Include(p => p.ProductName)
                .Include(p => p.Description)
                .Include(p => p.ProductImages)
                .Include(p => p.ProductAttributes)
                .Include(p => p.ProductOptions)
                .OrderByDescending(p => p.CreatedAt)
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync();

            var productResponses = mapper.Map<List<ProductResponse>>(products);

            return new()
            {
                Data = { productResponses },
                TotalCount = totalCount,
                PageNumber = request.PageNumber,
                PageSize = request.PageSize
            };
        }

        public override async Task<ProductResponse> CreateProduct(ProductRequest request, ServerCallContext context)
        {
            var product = mapper.Map<Entities.Product>(request);
            var created = await unitOfWork.ProductsRepository.CreateAsync(product);
            await unitOfWork.Complete();
            var response = mapper.Map<ProductResponse>(created);
            return response;
        }
        public override async Task<ProductResponse> DeleteProduct(ProductIdRequest request, ServerCallContext context)
        {
            var guid = request.Id.ToGuid();
            var product = await unitOfWork.ProductsRepository.FindAsync(x => x.Id == guid);
            await unitOfWork.ProductsRepository.DeleteAsync(guid);
            await unitOfWork.Complete();
            return mapper.Map<ProductResponse>(product);
        }

    }
}
