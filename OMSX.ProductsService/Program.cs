using OMSX.ProductsService.DI;
using OMSX.ProductsService.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register the database context with dependency injection
builder.Services.AddDBContext(builder.Configuration);
// Register the unit of work
builder.Services.RegisterUnitOfWork();
// Register repositories
builder.Services.RegisterRepositories();
builder.Services.RegisterMapster();
builder.Services.AddGrpc();
builder.Services.AddGrpcReflection();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapGrpcReflectionService();
app.MapGrpcService<ProductsGrpcService>().EnableGrpcWeb();
app.MapGrpcService<CategoriesGrpcService>().EnableGrpcWeb();


app.Run();
