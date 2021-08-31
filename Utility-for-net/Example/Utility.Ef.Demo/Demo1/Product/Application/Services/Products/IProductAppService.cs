using Product.Application.Services.Products;
using Product.Domain.Entities;
using Product.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using Utility.Application.Services;

namespace Product.Application.Services.Products
{
    public interface IProductAppService: ICrudAppService<IProductRepository, CreateProductInput, UpdateProductInput, GetProductInput, GetProductOutput, ProductEntity, long>
    {
        IList<LeftHotProductOutput> LeftHotProducts(int size = 10);

        IList<LeftHotProductOutput> HotProducts(int size = 10);

        IList<LeftHotProductOutput> NewProducts(int size = 10);

        IList<LeftHotProductOutput> SpecialPriceProducts(int size = 10);
    }
}
