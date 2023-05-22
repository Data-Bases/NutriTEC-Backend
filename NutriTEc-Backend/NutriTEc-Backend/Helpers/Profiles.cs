using AutoMapper;
using NutriTEc_Backend.Dtos;
using NutriTEc_Backend.Repository.DataModel;

namespace NutriTEc_Backend.Helpers
{
    public class Profiles
    {
        public static Mapper InitializeAutomapper()
        {
            
            var config = new MapperConfiguration(cfg =>
            {
                
                cfg.CreateMap<Administrator, AdminDto>();
                cfg.CreateMap<AdminDto, Administrator>();

            });
            
            var mapper = new Mapper(config);
            return mapper;
        }
    }
}
