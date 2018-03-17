using System.Collections.Generic;
using Athene.Inventory.Abstractions.Models;
using Athene.Inventory.Web.Areas.Librarian.Models.BooksViewModels;
using Athene.Inventory.Web.Areas.Librarian.Models.UsersViewModels;
using Athene.Inventory.Web.Models;
using Athene.Inventory.Web.ViewModels;
using AutoMapper;

namespace Athene.Inventory.Web.Mappers
{
    public static class SharedMappings 
    {
        private static IMapper Mapper;

        static SharedMappings()
        {
            Mapper = new MapperConfiguration(cfg => cfg.AddProfile<SharedMapperProfile>())
                .CreateMapper();
        }

        public static AuthorViewModel ToViewModel(this Author authors)
        {
            return Mapper.Map<AuthorViewModel>(authors);
        }

        public static IEnumerable<AuthorViewModel> ToViewModels(this IEnumerable<Author> authors)
        {
            return Mapper.Map<IEnumerable<AuthorViewModel>>(authors);
        }

        public static PublisherViewModel ToViewModel(this Publisher publishers)
        {
            return Mapper.Map<PublisherViewModel>(publishers);
        }

        public static IEnumerable<PublisherViewModel> ToViewModels(this IEnumerable<Publisher> publishers)
        {
            return Mapper.Map<IEnumerable<PublisherViewModel>>(publishers);
        }

        public static Author ToEntity(this CreateAuthorViewModel viewModel)
        {
            return Mapper.Map<Author>(viewModel);
        }

        public static Publisher ToEntity(this CreatePublisherViewModel viewModel)
        {
            return Mapper.Map<Publisher>(viewModel);
        }

        public static Category ToEntity(this CreateCategoryViewModel viewModel)
        {
            return Mapper.Map<Category>(viewModel);
        }

        public static CategoryViewModel ToViewModel(this Category category)
        {
            return Mapper.Map<CategoryViewModel>(category);
        }

        public static IEnumerable<CategoryViewModel> ToViewModels(this IEnumerable<Category> categories)
        {
            return Mapper.Map<IEnumerable<CategoryViewModel>>(categories);
        }

        public static ApplicationUser ToEntity(this CreateUserViewModel viewModel)
        {
            return Mapper.Map<ApplicationUser>(viewModel);
        }

        public static InventoryItem ToModel(this CreateInventoryItemViewModel viewModel)
        {
            return Mapper.Map<InventoryItem>(viewModel);
        }

        public static ApplicationUser ToModel(this CreateUserViewModel viewModel)
        {
            return Mapper.Map<ApplicationUser>(viewModel);
        }

        public static EditBookViewModel ToEditViewModel(this Book model)
        {
            return Mapper.Map<EditBookViewModel>(model);
        }

        public static Book ToModel(this EditBookViewModel viewModel)
        {
            return Mapper.Map<Book>(viewModel);
        }

        public static IEnumerable<InventoryItemDetailsViewModel> ToDetailsViewModels(this IEnumerable<InventoryItem> model)
        {
            return Mapper.Map<IEnumerable<InventoryItemDetailsViewModel>>(model);
        }
    }
}