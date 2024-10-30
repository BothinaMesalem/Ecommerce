using Ecommerce.Models;

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



    }
}
