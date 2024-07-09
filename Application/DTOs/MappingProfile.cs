using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs.Entries;
using AutoMapper;
using Core.Entities.MongoDB;

namespace Application.DTOs
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            CreateMap<ProductInput, ProductCollection>().ReverseMap();
            CreateMap<ProductOutput, ProductCollection>().ReverseMap();
            CreateMap<ShoppingCart, ShoppingCartCollection>().ReverseMap();
            CreateMap<ProductInCart, ProductInCartCollection>().ReverseMap();
            CreateMap<InvoiceOutput, InvoiceCollection>().ReverseMap();
            CreateMap<CustomerOutput, CustomerCollection>().ReverseMap();
            CreateMap<TransactionOutput, TransactionCollection>().ReverseMap();
        }
    }
}
