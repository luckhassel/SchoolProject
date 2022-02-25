﻿    using AutoMapper;
using CourseLibrary.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1.Profiles
{
    public class CourseProfiles:Profile
    {
        public CourseProfiles()
        {
            CreateMap<Course, CourseDTO>();
            CreateMap<CourseForCreationDTO, Course>();
        }
    }
}
