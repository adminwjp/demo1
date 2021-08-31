using Product.Application.Services.Dtos;
using Product.Application.Services.ProductCatagories;
using Product.Domain.Entities;
using Product.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Utility.Application.Services;

namespace Product.Application.Services.ProductCatagories
{
    public interface IProductCatagoryAppService: ICrudAppService<IProductCatagoryRepository, CreateProductCatagoryInput, UpdateProductCatagoryInput, GetProductCatagoryInput, GetProductCatagoryOutput, ProductCatagoryEntity, long>
    {
        IList<ProductCatagoryOutput> Catagories();
        List<NavgationOutput> NavgationCatagories();
        IList<BottomOutput> Bottom();
        IList<BottomNavgationOutput> BottomLink();
        List<CatagoryDto> FindCatagory();
    }
}
