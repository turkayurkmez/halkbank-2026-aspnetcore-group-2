using ECommerce.API.Models.Requests;
using ECommerce.API.Models.Responses;
using ECommerce.Infrastructure.Entities;
using Mapster;

namespace ECommerce.API.Mapping
{
    public class ProductMappingConfig : IRegister
    {

        /*
         * Bu class'ın amacı, eşleşme kurallarını tek bir yerde toplamak. 
         * Mapster varsayılan olarak aynı isimdeki ve tipteki değerleri birbirlerine dönüştürür.
         * Peki ya bu varsayılan senaryo size etmiyorsa?
         */
        public void Register(TypeAdapterConfig config)
        {

            /*Aşağıdakilerden hiçbirini yazmak zorunda değildim. Eğer isimler eşleşmiyorsa ya da iç içe obje senaryosu gibi takla atmanız gereken işler varsa config o zaman zorunludur.  */
            config.NewConfig<Product, ProductListResponse>()
                  .Map(dest => dest.CategoryName, src => src.Category != null ? src.Category.Name : string.Empty);

            config.NewConfig<CreateProductRequest, Product>()
                  .Ignore(dest => dest.Id, dest => dest.Category, dest => dest.Reviews);

        }
    }
}
