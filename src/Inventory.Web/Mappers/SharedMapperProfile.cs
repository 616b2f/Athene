using System.Linq;
using Athene.Inventory.Abstractions.Models;
using Athene.Inventory.Web.Areas.Librarian.Models.BooksDto;
using Athene.Inventory.Web.Areas.Librarian.Models.UsersDto;
using Athene.Inventory.Web.Dto;
using AutoMapper;

namespace Athene.Inventory.Web.Mappers
{
    public class SharedMapperProfile : Profile
    {
        public SharedMapperProfile()
        {
            CreateMap<Author, AuthorDto>();

            CreateMap<Publisher, PublisherDto>();

            CreateMap<CreateAuthorDto, Author>();

            CreateMap<CreatePublisherDto, Publisher>();

            CreateMap<Category, CategoryDto>();

            CreateMap<CreateUserDto, User>()
                .ForMember(x => x.Address, opt => opt.MapFrom(x => new Address(x.AddressStreet, x.AddressZip, x.AddressCity, x.AddressCountry)));

            CreateMap<CreateInventoryItemDto, InventoryItem>()
                .ForMember(x => x.StockLocation, opt => opt.MapFrom(x => new StockLocation{ Hall = x.Hall, Corridor = x.Corridor, Rack = x.Rack, Level = x.Level, Position = x.Position }));

            CreateMap<EditBookDto, Book>()
                .ForMember(x => x.Authors, opt => opt.MapFrom(x => x.AuthorsIds.Select(i => new Author { Id = i })))
                .ForMember(x => x.Categories, opt => opt.MapFrom(x => x.CategoriesIds.Select(i => new Category { Id = i })));

            CreateMap<Book, EditBookDto>();

            CreateMap<InventoryItem, InventoryItemDetailsDto>()
                .ForMember(x => x.InventoryItemId, opt => opt.MapFrom(x => x.Id))
                .ForMember(x => x.StockLocation, opt => opt.MapFrom(x => x.StockLocation.OneLiner))
                .ForMember(x => x.RentedByUserDisplayName, opt => opt.MapFrom(x => x.RentedBy.FullName))
                .ForMember(x => x.RentedByUserId, opt => opt.MapFrom(x => x.RentedBy.Id));
        }
    }
}