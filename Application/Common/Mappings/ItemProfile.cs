
using AutoMapper;
using Common.DTOs.Requests;
using Common.DTOs.Responses;
using Domain.Entities;

namespace Application.Common.Mappings
{
    public class ItemProfile : Profile
    {
        public ItemProfile()
        {
            CreateMap<CreateItemRequest, Item>();
            CreateMap<Item, CreateItemResponse>();

            CreateMap<UpdateItemRequest, Item>();
            CreateMap<Item, UpdateItemResponse>().ReverseMap();

            CreateMap<Item, DeleteItemResponse>();

            CreateMap<Item, GetItemByIdResponse>();
            CreateMap<Item, GetItemsResponse>();
        }
        
    }
}
