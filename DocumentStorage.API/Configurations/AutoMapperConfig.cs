using AutoMapper;
using DocumentStorage.API.DTOs;
using DocumentStorage.API.Helpers;
using DocumentStorage.API.Models;

namespace DocumentStorage.API.Configurations
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<DocumentDto, Document>()
                .ForMember(dest => dest.Tags, opt => opt
                    .MapFrom(source => source.Tags!.ConvertStringListToTagIEnumerable()))
                .ForMember(dest => dest.Data, opt => opt
                    .MapFrom(source => source.Data.ConvertDictionaryToJSONString()));

            CreateMap<Document, DocumentDto>()
                .ForMember(dest => dest.Tags, opt => opt
                    .MapFrom(source => source.Tags!.ConvertTagsToStringIEnumerable()))
                .ForMember(dest => dest.Data, opt => opt
                    .MapFrom(source => source.Data.ConvertJSONStringToDictionary()));

            CreateMap<Tag, TagDto>().ReverseMap();         
        }
    }
}
