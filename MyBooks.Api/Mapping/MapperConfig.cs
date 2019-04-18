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
                        opt => opt.MapFrom(src => $@"http://localhost:5000/covers/{src.CoverImagePath}"))
                    .ForMember(dto => dto.GoodreadsUrl,
                        opt => opt.MapFrom(src => $@"https://www.goodreads.com/book/show/{src.GoodreadsId}"));
                cfg.CreateMap<Dto.Dtos.Book, Book>();

                // TODO: CreateMap for other entities and dtos as well
            });

            var mapper = config.CreateMapper();
            return mapper;
        }
    }
}
