using AutoMapper;
using GDATemplate.Application.DTO.Entities;
using GDATemplate.Domain.Entities;

namespace GDATemplate.Application.Mapping
{
    public class AutoMapperSetup : Profile
    {
        public AutoMapperSetup()
        {
            MappingDtoToModel();
        }

        private void MappingDtoToModel()
        {
            CreateMap<DemoDTO, Demo>().ReverseMap();
            CreateMap<ExampleRelationshipDTO, ExampleRelationship>().ReverseMap();
        }
    }
}
