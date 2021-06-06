using System.Collections.Generic;
using Athene.Inventory.Abstractions.Models;
using Athene.Inventory.Web.Areas.Librarian.Models.BooksDto;
using Athene.Inventory.Web.Areas.Librarian.Models.UsersDto;
using Athene.Inventory.Web.Dto;
using AutoMapper;

namespace Athene.Inventory.Web.Mappers
{
    public static class SharedMappings 
    {
        private static readonly IMapper Mapper;

        static SharedMappings()
        {
            Mapper = new MapperConfiguration(cfg => cfg.AddProfile<SharedMapperProfile>())
                .CreateMapper();
        }

        public static AuthorDto ToDto(this Author authors)
        {
            return Mapper.Map<AuthorDto>(authors);
        }

        public static IEnumerable<AuthorDto> ToDto(this IEnumerable<Author> authors)
        {
            return Mapper.Map<IEnumerable<AuthorDto>>(authors);
        }

        public static PublisherDto ToDto(this Publisher publishers)
        {
            return Mapper.Map<PublisherDto>(publishers);
        }

        public static IEnumerable<PublisherDto> ToDto(this IEnumerable<Abstractions.Models.Publisher> publishers)
        {
            return Mapper.Map<IEnumerable<PublisherDto>>(publishers);
        }

        public static Author ToEntity(this CreateAuthorDto dto)
        {
            return Mapper.Map<Author>(dto);
        }

        public static Abstractions.Models.Publisher ToEntity(this CreatePublisherDto dto)
        {
            return Mapper.Map<Abstractions.Models.Publisher>(dto);
        }

        public static Category ToEntity(this CreateCategoryDto dto)
        {
            return Mapper.Map<Category>(dto);
        }

        public static CategoryDto ToDto(this Category category)
        {
            return Mapper.Map<CategoryDto>(category);
        }

        public static IEnumerable<CategoryDto> ToDto(this IEnumerable<Category> categories)
        {
            return Mapper.Map<IEnumerable<CategoryDto>>(categories);
        }

        public static User ToEntity(this CreateUserDto dto)
        {
            return Mapper.Map<User>(dto);
        }

        public static InventoryItem ToModel(this CreateInventoryItemDto dto)
        {
            return Mapper.Map<InventoryItem>(dto);
        }

        public static User ToModel(this CreateUserDto dto)
        {
            return Mapper.Map<User>(dto);
        }

        public static EditBookDto ToEditDto(this Book model)
        {
            return Mapper.Map<EditBookDto>(model);
        }

        public static void ToModel(this EditBookDto dto, Book book)
        {
            Mapper.Map<EditBookDto,Book>(dto, book);
        }

        public static IEnumerable<InventoryItemDetailsDto> ToDetailsDto(this IEnumerable<InventoryItem> model)
        {
            return Mapper.Map<IEnumerable<InventoryItemDetailsDto>>(model);
        }
    }
}