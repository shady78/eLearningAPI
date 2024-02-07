using AutoMapper;
using eLearningAPI.DTO;
using eLearningAPI.Models;

namespace eLearningAPI.Core.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //Video 
            CreateMap<Video, AddVideoDto>()
                .ForMember(dest => dest.LessonId, opt => opt.MapFrom(src => src.LessonId)).ReverseMap();
        }
    }
}
