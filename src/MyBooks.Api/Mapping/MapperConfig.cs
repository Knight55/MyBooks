using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using MyBooks.Bll.Entities;

namespace MyBooks.Api.Mapping
{
    /// <summary>
    /// 
    /// </summary>
    public class MapperConfig
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static IMapper Configure()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AllowNullCollections = true;

                // Book
                cfg.CreateMap<Book, Dto.Dtos.Book>()
                    .ForMember(dto => dto.AuthorIds,
                        opt => opt.MapFrom(src =>
                            new List<int>(src.BookAuthors.Select(ba => ba.AuthorId))))
                    .ForMember(dto => dto.CoverUrl,
                        opt => opt.MapFrom(src =>
                            $@"http://localhost:5000/covers/{src.CoverImagePath}"))
                    .ForMember(dto => dto.GoodreadsUrl,
                        opt => opt.MapFrom(src =>
                            $@"https://www.goodreads.com/book/show/{src.GoodreadsId}"));
                cfg.CreateMap<Dto.Dtos.Book, Book>()
                    .ForMember(entity => entity.CoverImagePath,
                        opt => opt.MapFrom(src =>
                            src.CoverUrl == null ? "" : src.CoverUrl.Substring(src.CoverUrl.LastIndexOf(@"/", StringComparison.Ordinal) + 1)))
                    .ForMember(entity => entity.GoodreadsId,
                        opt => opt.MapFrom(src =>
                            src.GoodreadsUrl == null ? "" : src.GoodreadsUrl.Substring(src.GoodreadsUrl.LastIndexOf(@"/", StringComparison.Ordinal) + 1)));

                // Author
                cfg.CreateMap<Author, Dto.Dtos.Author>()
                    .ForMember(dto => dto.BookIds,
                        opt => opt.MapFrom(src =>
                            new List<int>(src.BookAuthors.Select(ba => ba.BookId))));
                cfg.CreateMap<Dto.Dtos.Author, Author>();
            });

            var mapper = config.CreateMapper();
            return mapper;
        }
    }
}
