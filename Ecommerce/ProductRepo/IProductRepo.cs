using Ecommerce.Models;

namespace Ecommerce.ProductRepo
{
    public interface IProductRepo
    {
        public Task<IEnumerable<AllProductDto>> GetALL();

        public Task<Product> GetById(int id); 

        public Task<Product> Delete(int id);

        public Task Add(ProductDto productDto);

        public Task Update(ProductDto productDto,int id);

        public  Task<List<AllProductDto>> GetALLbysellerId(int id);



    }
}
