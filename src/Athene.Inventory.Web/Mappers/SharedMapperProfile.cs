using System.Linq;
using Athene.Inventory.Abstractions.Models;
using Athene.Inventory.Web.Areas.Librarian.Models.BooksViewModels;
using Athene.Inventory.Web.Areas.Librarian.Models.UsersViewModels;
using Athene.Inventory.Web.Models;
using Athene.Inventory.Web.ViewModels;
using AutoMapper;

namespace Athene.Inventory.Web.Mappers
{
    public class SharedMapperProfile : Profile
    {
        public SharedMapperProfile()
        {
            CreateMap<Author, AuthorViewModel>();

            CreateMap<Publisher, PublisherViewModel>();

            CreateMap<CreateAuthorViewModel, Author>();

            CreateMap<CreatePublisherViewModel, Publisher>();

            CreateMap<Category, CategoryViewModel>();

            CreateMap<CreateUserViewModel, ApplicationUser>()
                .ForMember(x => x.Address, opt => opt.MapFrom(x => new Address(x.AddressStreet, x.AddressZip, x.AddressCity, x.AddressCountry)));

            CreateMap<CreateInventoryItemViewModel, InventoryItem>()
                .ForMember(x => x.StockLocation, opt => opt.MapFrom(x => new StockLocation{ Hall = x.Hall, Corridor = x.Corridor, Rack = x.Rack, Level = x.Level, Position = x.Position }));

            CreateMap<EditBookViewModel, Book>()
                .ForMember(x => x.Authors, opt => opt.MapFrom(x => x.authorsIds.Select(i => new Author { Id = i })))
                .ForMember(x => x.Categories, opt => opt.MapFrom(x => x.categoriesIds.Select(i => new Category { Id = i })));

            CreateMap<Book, EditBookViewModel>();

            CreateMap<InventoryItem, InventoryItemDetailsViewModel>()
                .ForMember(x => x.InventoryItemId, opt => opt.MapFrom(x => x.Id))
                .ForMember(x => x.StockLocation, opt => opt.MapFrom(x => x.StockLocation.OneLiner))
                .ForMember(x => x.RentedByUserDisplayName, opt => opt.MapFrom(x => x.RentedBy.FullName))
                .ForMember(x => x.RentedByUserId, opt => opt.MapFrom(x => x.RentedBy.Id));
        }
    }
}