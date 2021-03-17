using AutoMapper;
using BL = byin_netcore_business.Entity;
using DL = byin_netcore_data.Model;

namespace byin_netcore_data.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<BL.Product, DL.Product>();
            CreateMap<DL.Product, BL.Product>();

            CreateMap<BL.ProductAndCategory, DL.ProductAndCategory>();
            CreateMap<DL.ProductAndCategory, BL.ProductAndCategory>();

            CreateMap<BL.ProductAndImg, DL.ProductAndImg>();
            CreateMap<DL.ProductAndImg, BL.ProductAndImg>();

            CreateMap<BL.Order, DL.Order>();
            CreateMap<DL.Order, BL.Order>();

            CreateMap<BL.OrderEntity, DL.OrderEntity>();
            CreateMap<DL.OrderEntity, BL.OrderEntity>();

            CreateMap<BL.ProductCategory, DL.ProductCategory>();
            CreateMap<DL.ProductCategory, BL.ProductCategory>();

            CreateMap<BL.File.FilePath, DL.FilePath>();
            CreateMap<DL.FilePath, BL.File.FilePath>();
        }
    }
}
