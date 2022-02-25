using AutoMapper;
using CourseLibrary.API.Entities;
using CourseLibrary.API.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Helpers;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("api/authors")]
    public class AuthorsController : ControllerBase
    {
        private readonly ICourseLibraryRepository _repo;
        private readonly IMapper _mapper;

        public AuthorsController(ICourseLibraryRepository repository, IMapper mapper)
        {
            _repo = repository ?? throw new NullReferenceException(nameof(repository));
            _mapper = mapper ?? throw new NullReferenceException(nameof(mapper));
        }

        [HttpGet()]
        [HttpHead]
        public ActionResult<IEnumerable<AuthorDTO>> GetAuthors(
            [FromQuery(Name ="category")] string mainCategory,
            [FromQuery(Name="search")] string searchQuery)
        {
            var authors = _repo.GetAuthors(mainCategory, searchQuery);

            return Ok(_mapper.Map<IEnumerable<AuthorDTO>>(authors));
        }

        [HttpGet("{authorId}", Name = "GetAuthor")]
        public ActionResult<AuthorDTO> GetAuthor([FromRoute] Guid authorId)
        {
            var author = _repo.GetAuthor(authorId);

            if (author is null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<AuthorDTO>(author));
        }

        [HttpPost]
        public ActionResult<AuthorDTO> CreateAuthor(AuthorForCreationDTO author)
        {
            var authorEntity = _mapper.Map<Author>(author);
            _repo.AddAuthor(authorEntity);
            _repo.Save();

            var authorToReturn = _mapper.Map<AuthorDTO>(authorEntity);
            return CreatedAtRoute("GetAuthor", new { authorId = authorToReturn.Id }, authorToReturn);
        }

        [HttpOptions]
        public ActionResult GetAuthorOptions()
        {
            Request.Headers.Add("Allow", "GET, POST, OPTIONS");
            return Ok();
        }
    }
}
