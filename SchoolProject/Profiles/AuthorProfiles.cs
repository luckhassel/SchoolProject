﻿using AutoMapper;
using CourseLibrary.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Helpers;
using WebApplication1.Models;

namespace WebApplication1.Profiles
{
    public class AuthorProfiles:Profile
    {
        public AuthorProfiles()
        {
            CreateMap<Author, AuthorDTO>()
                .ForMember(
                dest => dest.Name,
                opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"))
                .ForMember(
                dest => dest.Age,
                opt => opt.MapFrom(src => src.DateOfBirth.GetCurrentAge())
                );

            CreateMap<AuthorForCreationDTO, Author>();
        }
    }
}
