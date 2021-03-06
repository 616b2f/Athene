using Athene.Inventory.Abstractions.Models;
using Athene.Inventory.Web.Areas.Admin.Models;
using AutoMapper;

namespace Athene.Inventory.Web.Mappers
{
    public static class StudentMappings 
    {
        private static IMapper Mapper;

        static StudentMappings ()
        {
            Mapper = new MapperConfiguration(cfg => cfg.AddProfile<StudentMapperProfile>())
                .CreateMapper();
        }

        public static StudentViewModel ToViewModel(this IUser users)
        {
            return Mapper.Map<StudentViewModel>(users);
        }
    }
}