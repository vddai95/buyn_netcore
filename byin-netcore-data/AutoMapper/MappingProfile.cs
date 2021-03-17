using AutoMapper;
using BL = byin_netcore_business.Entity;
using DL = byin_netcore_data.Model;

namespace byin_netcore_data.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<BL.Product, DL.Product>().ReverseMap();

            CreateMap<BL.ProductAndCategory, DL.ProductAndCategory>().ReverseMap();

            CreateMap<BL.ProductAndImg, DL.ProductAndImg>().ReverseMap();

            CreateMap<BL.Order, DL.Order>().ReverseMap();

            CreateMap<BL.OrderEntity, DL.OrderEntity>().ReverseMap();

            CreateMap<BL.ProductCategory, DL.ProductCategory>().ReverseMap();

            CreateMap<BL.File.FilePath, DL.FilePath>().ReverseMap();
        }
    }
}
