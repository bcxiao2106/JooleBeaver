using AutoMapper;
using JooleWebAPI.DTO;

namespace JooleWebAPI.App_Start
{
    public class AutoMapperConfig
    {
        public static void Initialize()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<tblState, StateDTO>();
                cfg.CreateMap<tblCity, CityDTO>();
                cfg.CreateMap<ProjectDTO, tblProject>();
                cfg.CreateMap<tblProject, ProjectDTO>();
                cfg.CreateMap<CategoryDTO, tblCategory>();
                cfg.CreateMap<SubCategoryDTO, tblSubCategory>();
                cfg.CreateMap<tblProduct, ProductDTO>();
                cfg.CreateMap<ProductPropertyDTO, ProductDTO>();
                cfg.CreateMap<tblProperty, ProductPropertyDTO>();
                cfg.CreateMap<tblPropertyValue, ProductPropertyDTO>();
                cfg.CreateMap<tblUser, UserDTO>();
                cfg.CreateMap<tblTechSpecFilter, TechSpecFilterDTO>();
            });
        }
    }
}