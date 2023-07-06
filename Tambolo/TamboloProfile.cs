using AutoMapper;
using Tambolo.Dtos;
using Tambolo.Models;

namespace Tambolo
{
    public class TamboloProfile: Profile
    {
        public TamboloProfile()
        {
            CreateMap<Category, CategoryResponse>().ReverseMap();
            CreateMap<CategoryRequest, Category>();

            CreateMap<Product, ProductResponse>().ReverseMap();
            CreateMap<ProductRequest, Product>();
            CreateMap<ProductUpdateRequest, Product>();

            CreateMap<Order, OrderResponse>().ReverseMap();
            CreateMap<OrderRequest, Order>();
        }
    }
}
