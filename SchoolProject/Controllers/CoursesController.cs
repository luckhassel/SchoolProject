using AutoMapper;
using CourseLibrary.API.Entities;
using CourseLibrary.API.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("api/authors/{authorId}/courses")]
    public class CoursesController : ControllerBase
    {
        private readonly ICourseLibraryRepository _repo;
        private readonly IMapper _mapper;

        public CoursesController(ICourseLibraryRepository repository, IMapper mapper)
        {
            _repo = repository ?? throw new NullReferenceException(nameof(repository));
            _mapper = mapper ?? throw new NullReferenceException(nameof(mapper));
        }

        [HttpGet()]
        public ActionResult<IEnumerable<CourseDTO>> GetCourses(Guid authorId)
        {
            if (!_repo.AuthorExists(authorId))
            {
                return NotFound();
            }

            var courses = _repo.GetCourses(authorId);

            return Ok(_mapper.Map<IEnumerable<CourseDTO>>(courses));
        }

        [HttpGet("{courseId}", Name = "GetCourseForAuthor")]
        public ActionResult<IEnumerable<CourseDTO>> GetCourse([FromRoute] Guid authorId, [FromRoute] Guid courseId)
        {
            if (!_repo.AuthorExists(authorId))
            {
                return NotFound();
            }

            var course = _repo.GetCourse(authorId, courseId);

            if (course == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<CourseDTO>(course));
        }

        [HttpPost]
        public ActionResult<CourseDTO> CreateCourseForAuthor(Guid authorId, CourseForCreationDTO course)
        {
            if (!_repo.AuthorExists(authorId))
            {
                return NotFound();
            }

            var courseEntity = _mapper.Map<Course>(course);
            _repo.AddCourse(authorId, courseEntity);
            _repo.Save();

            var courseToReturn = _mapper.Map<CourseDTO>(courseEntity);
            return CreatedAtRoute("GetCourseForAuthor",
                new { authorId = courseToReturn.AuthorId, courseId = courseToReturn.Id }, courseToReturn);
        }
    }
}
