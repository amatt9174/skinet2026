using System;
using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class ProductRepository(StoreContext context) : IProductRepository
{
    public void AddProduct(Product product)
    {
        context.Products.Add(product);
    }

    public void DeleteProduct(Product product)
    {
        context.Products.Remove(product);
    }

    public async Task<IReadOnlyList<string>> GetProductBrandsAsync()
    {
        return await context.Products.Select(p => p.ProductBrand)
            .Distinct().ToListAsync();
    }

    public async Task<Product?> GetProductByIdAsync(int id)
    {
        return await context.Products.FindAsync(id);

    }

    // public async Task<IReadOnlyList<Product>> GetProductsAsync()
    // {
    //     return await context.Products.ToListAsync<Product>();
    // }

    public async Task<IReadOnlyList<string>> GetProductTypesAsync()
    {
        return await context.Products.Select(p => p.ProductType)
            .Distinct().ToListAsync();
    }

    public bool ProductExists(int id)
    {
        return context.Products.Any(p => p.Id == id);       
    }

    public async Task<bool> SaveChangesAsync()
    {
        return await context.SaveChangesAsync() > 0;
    }

    public void UpdateProduct(Product product)
    {
        context.Entry(product).State = EntityState.Modified;
    }

    public async Task<IReadOnlyList<Product>> GetProductsAsync(string? brand, string? type, string? sort)
    {
        var query = context.Products.AsQueryable();
        if (!string.IsNullOrEmpty(brand))
        {
            query = query.Where(p => p.ProductBrand == brand);
        }
        if (!string.IsNullOrEmpty(type))
        {
            query = query.Where(p => p.ProductType == type);
        }
        
        query = sort switch
        {
            "priceAsc" => query.OrderBy(p => p.Price),
            "priceDesc" => query.OrderByDescending(p => p.Price),
            _ => query.OrderBy(p => p.Name)
        };
        
        return await query.ToListAsync();
    }
}