using App.Application.Features.Products;
using App.Application.Features.Products.Create;
using App.Application.Features.Products.Update;
using App.Application.Features.Products.UpdateStock;
using App.Domain.Entities;
using CleanApp.Api.Filters;
using Microsoft.AspNetCore.Mvc;

namespace CleanApp.API.Controllers;

public class ProductsController(IProductService productService) : CustomBaseController
{
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var productResult = await productService.GetAllListAsync();

        return CreateActionResult(productResult);
    }

    [HttpGet("{pageNumber:int}/{pageSize:int}")]
    public async Task<IActionResult> GetPagedAll(int pageNumber, int pageSize)
    {
        var productResult = await productService.GetPagedAllListAsync(pageNumber, pageSize);

        return CreateActionResult(productResult);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var productResult = await productService.GetByIdAsync(id);

        return CreateActionResult(productResult);
    }
    [HttpPost]
    public async Task<IActionResult> Create(CreateProductRequest request)
    {
        return CreateActionResult(await productService.CreateAsync(request));
    }
    
    [ServiceFilter(typeof(NotFoundFilter<Product, int>))]
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, UpdateProductRequest request)
    {
        var productResult = await productService.UpdateAsync(id, request);

        return CreateActionResult(productResult);
    }

    [HttpPatch("stock")]
    public async Task<IActionResult> UpdateStock(UpdateProductStockRequest request)
    {
        var productResult = await productService.UpdateStockAsync(request);

        return CreateActionResult(productResult);
    }

    [ServiceFilter(typeof(NotFoundFilter<Product,int>))]
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var productResult = await productService.DeleteAsync(id);

        return CreateActionResult(productResult);
    }
}
