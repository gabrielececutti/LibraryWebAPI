using AutoMapper;
using LibraryPersistenceLayer.Models;
using LibraryWebApi.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryBusinessLogic.AutoMappersProfiles;

public class MainProfile : Profile
{
    public MainProfile()
    {
        CreateMap<Author, AuthorDto>();

        CreateMap<AuthorDto, Author>()
            .ForMember(dest => dest.Books, opt => opt.Ignore())
            .ReverseMap();

        CreateMap<Book, BookResponseDTO>();
        CreateMap<BookInsertDTO, Book>();
    }
}
