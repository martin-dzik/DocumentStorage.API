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
            CreateMap<CreateDocumentDto, Document>()
                .ForMember(dest => dest.Tags, opt => opt
                    .MapFrom(source => source.Tags!.ConvertStringCollectionToTagIEnumerable()));

            CreateMap<Document, DocumentDto>()
                .ForMember(dest => dest.Tags, opt => opt
                    .MapFrom(source => source.Tags!.ConvertTagCollectionToStringIEnumerable()));
   
            CreateMap<DocumentDto, Document>()
                .ForMember(dest => dest.Tags, opt => opt
                    .MapFrom(source => source.Tags!.ConvertStringCollectionToTagIEnumerable()));

            CreateMap<Tag, TagDto>().ReverseMap();         
        }
    }
}
