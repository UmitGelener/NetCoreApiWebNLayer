﻿using App.Repositories;
using App.Repositories.Products;
using App.Services.Products.Create;
using App.Services.Products.Update;
using App.Services.Products.UpdateStock;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace App.Services.Products;

public class ProductService(IProductRepository productRepository,IUnitofWork unitofWork,IMapper mapper) : IProductService
{
    public async Task<ServiceResult<List<ProductDto>>> GetTopPriceProductAsync(int count)
    {
        var products = await productRepository.GetTopPriceProductAsync(count);

        //var productsDto = products.Select(p => new ProductDto(p.Id,p.Name,p.Price,p.Stock)).ToList();

        var productsDto = mapper.Map<List<ProductDto>>(products);


        return new ServiceResult<List<ProductDto>>()
        {
            Data = productsDto
        };
    }

    public async Task<ServiceResult<List<ProductDto>>> GetAllListAsync()
    {
        var products = await productRepository.GetAll().ToListAsync();


        //var productsAsDto = products.Select(p => new ProductDto(p.Id, p.Name, p.Price, p.Stock)).ToList();

        var productsAsDto = mapper.Map<List<ProductDto>>(products);
        return ServiceResult<List<ProductDto>>.Success(productsAsDto);

    }

    public async Task<ServiceResult<List<ProductDto>>> GetPagedAllListAsync(int pageNumber, int pageSize)
    {
        int skip = (pageNumber - 1) * pageSize;
        var products = await productRepository.GetAll().Skip(skip).Take(pageSize).ToListAsync();

        //var productsAsDto = products.Select(p => new ProductDto(p.Id, p.Name, p.Price, p.Stock)).ToList();

        var productsAsDto = mapper.Map<List<ProductDto>>(products);

        return ServiceResult<List<ProductDto>>.Success(productsAsDto);
    }

    public async Task<ServiceResult<ProductDto?>> GetByIdAsync(int id)
    {
        var products = await productRepository.GetByIdAsync(id);
        if(products is null)
        {
            return ServiceResult<ProductDto?>.Fail("Product Not Found", HttpStatusCode.NotFound);
        }
        /*
        var productsDto = new ProductDto(products!.Id, products.Name, products.Price, products.Stock);
        */
        var productsDto = mapper.Map<ProductDto>(products);

        return ServiceResult<ProductDto>.Success(productsDto)!;
    }

    public async Task<ServiceResult<CreateProductResponse>> CreateAsync(CreateProductRequest request)
    {

        //throw new CriticalException("Kritik Seviye hata meydana geldi");

        var anyProducts = await productRepository.Where(x => x.Name == request.Name).AnyAsync();
        if (anyProducts)
        {
            return ServiceResult<CreateProductResponse>.Fail("Ürün ismi veritabanında bulunmaktadır");
        }

        var product = mapper.Map<Product>(request);

        /*
        var product = new Product()
        {
            Name = request.Name,
            Price = request.Price,
            Stock = request.Stock
        };
        */
        await productRepository.AddAsync(product);
        await unitofWork.SaveChangesAsync();
        return ServiceResult<CreateProductResponse>.SuccessAsCreated(new CreateProductResponse(product.Id),$"api/products/{product.Id}");
    }

    public async Task<ServiceResult> UpdateAsync(int id ,UpdateProductRequest request)
    {

        var isProductNameExist = await productRepository.Where(x => x.Name == request.Name && x.Id != id).AnyAsync();
        if (isProductNameExist)
        {
            return ServiceResult.Fail("Ürün ismi veritabanında bulunmaktadır");
        }


        /*
        product!.Name = request.Name;
        product.Price = request.Price;
        product.Stock = request.Stock;
        */

        var product = mapper.Map<Product>(request);
        product.Id = id;

        productRepository.Update(product);
        await unitofWork.SaveChangesAsync();
        return ServiceResult.Success(HttpStatusCode.NoContent);
    }

    public async Task<ServiceResult> DeleteAsync(int id)
    {
        var product = await productRepository.GetByIdAsync(id);
        productRepository.Delete(product!);
        await unitofWork.SaveChangesAsync();
        return ServiceResult.Success(HttpStatusCode.NoContent);

    }

    public async Task<ServiceResult> UpdateStockAsync(UpdateProductStockRequest request)
    {
        var product = await productRepository.GetByIdAsync(request.ProductId);
        if (product is null)
        {
            return ServiceResult.Fail("Product Not Found", HttpStatusCode.NotFound);
        }

        product.Stock = request.Quantity;


        productRepository.Update(product);
        await unitofWork.SaveChangesAsync();
        return ServiceResult.Success(HttpStatusCode.NoContent);
    }
}

