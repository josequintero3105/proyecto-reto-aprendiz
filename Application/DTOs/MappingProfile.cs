using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Core.Entities.MongoDB;

namespace Application.DTOs
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            CreateMap<Product, ProductCollection>().ReverseMap();
            CreateMap<ProductToGet, ProductCollection>().ReverseMap();
            CreateMap<ShoppingCart, ShoppingCartCollection>().ReverseMap();
            CreateMap<ProductInCart, ProductInCartCollection>().ReverseMap();
            CreateMap<Invoice, InvoiceCollection>().ReverseMap();
            CreateMap<Customer, CustomerCollection>().ReverseMap();
        }
    }
}
