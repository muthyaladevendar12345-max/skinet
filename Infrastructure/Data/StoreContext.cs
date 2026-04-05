using System;
using Core.Entities;
using Infrastructure.Config;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class StoreContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<Product> Products { get; set; }
    

    override protected void OnModelCreating(ModelBuilder modelBuilder)
    {
    
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ProductConfiguration).Assembly);

        // modelBuilder.Entity<Product>().HasData(
        //     new Product
        //     {
        //         Id = 1,
        //         Name = "Angular Speedster Board 2000",
        //         Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Donec aliquet dolor libero, eget venenatis mauris finibus dictum. Vestibulum quis elit eget neque porttitor congue non sit amet dolor.",
        //         Price = 20000,
        //         PictureUrl = "/images/products/sb-ang1.png",
        //         Brand = "Angular",
        //         Type = "Boards",
        //         QuantityInStock = 100
        //     },
        //     new Product
        //     {
        //         Id = 2,
        //         Name = "Green Angular Board 3000",
        //         Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Donec aliquet dolor libero, eget venenatis mauris finibus dictum. Vestibulum quis elit eget neque porttitor congue non sit amet dolor.",
        //         Price = 15000,
        //         PictureUrl = "/images/products/sb-ang2.png",
        //         Brand = "Angular",
        //         Type = "Boards",
        //         QuantityInStock = 100
        //     },
        //     new Product
        //     {
        //         Id = 3,
        //         Name = "Core Blue Hat",
        //         Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Donec aliquet dolor libero, eget venenatis mauris finibus dictum. Vestibulum quis elit eget neque porttitor congue non sit amet dolor.",
        //         Price = 800,
        //         PictureUrl = "/images/products/hat-core1.png",
        //         Brand = "Core",
        //         Type = "Hats",
        //         QuantityInStock = 100
        //     }
        // );
    }
}
