using System;
using System.Linq;
using AutoMapper;
using MyBooks.Bll.Entities;

namespace MyBooks.Api.Mapping
{
    public class MapperConfig
    {
        public static IMapper Configure()
        {
            var config = new MapperConfiguration(cfg =>
            {
                //cfg.AllowNullCollections = true;
                cfg.CreateMap<Book, Dto.Dtos.Book>()
                    //.ForMember(dto => dto.Authors, opt => opt.Ignore())
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

                // TODO: CreateMap for other entities and dtos as well
            });

            var mapper = config.CreateMapper();
            return mapper;
        }
    }
}
