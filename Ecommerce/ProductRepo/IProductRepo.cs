﻿using Ecommerce.Models;

namespace Ecommerce.ProductRepo
{
    public interface IProductRepo
    {
        public Task<IEnumerable<AllProductDto>> GetALL();

        public Task<AllProductDto> GetById(int id); 

        public Task Delete(int id);

        public Task Add(ProductDto productDto);

        public Task Update(UpdateProductDto uproductDto,int id);

        public  Task<List<AllProductDto>> GetALLbysellerId(int id);

        public Task Editqty(ProductStackqtyDto productStackqtyDto,int id);

        public Task<List<SellerNamewithAllProductDto>> GetAllwithSellerName();

        public  Task<IEnumerable<AllProductDto>> getthefourproduct();


        public Task<IEnumerable<AllProductDto>> getthelastfourproduct();

        public  Task<int> GetCountProducts();

        public Task<int> GetCountProductsthataddedbyseller(int id);

        public Task<int> GetCountProductsthatinstockbyseller(int id);

        public Task<int> GetCountProductsthatoutstockbyseller(int id);
       

    }
}
