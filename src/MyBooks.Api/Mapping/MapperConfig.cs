using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using MyBooks.Dal.Entities;
using Rating = MyBooks.Dto.Dtos.Rating;

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
                    .ForMember(dto => dto.Rating,
                        opt => opt.MapFrom(src =>
                            src.Ratings.Count == 0 ? 0.0 : src.Ratings.Average(r => r.Value)));
                cfg.CreateMap<Dto.Dtos.Book, Book>();
                    //.ForMember(entity => entity.BookAuthors,
                    //    opt => opt.MapFrom(src =>
                    //        src.AuthorIds.Select(id => new BookAuthor {AuthorId = id, BookId = src.Id})));

                // Author
                cfg.CreateMap<Author, Dto.Dtos.Author>()
                    .ForMember(dto => dto.BookIds,
                        opt => opt.MapFrom(src =>
                            new List<int>(src.BookAuthors.Select(ba => ba.BookId))));
                cfg.CreateMap<Dto.Dtos.Author, Author>();

                // Rating
                cfg.CreateMap<Rating, Dto.Dtos.Rating>();
                cfg.CreateMap<Dto.Dtos.Rating, Rating>();
            });

            var mapper = config.CreateMapper();
            return mapper;
        }
    }
}
