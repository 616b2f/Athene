using Athene.Inventory.Abstractions.Models;
using Athene.Inventory.Web.Areas.Admin.Models;
using AutoMapper;

namespace Athene.Inventory.Web.Mappers
{
    public class StudentMapperProfile : Profile
    {
        public StudentMapperProfile()
        {
            CreateMap<IUser, StudentViewModel>()
                .ForMember(s => s.SchoolClass, opt => opt.MapFrom(u => u.Student.SchoolClass.Name ?? ""))
                .ForMember(s => s.SchoolName, opt => opt.MapFrom(u => u.Student.SchoolClass.School.Name ?? ""));
        }
    }
}