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
    public class AuthorsCollectionController : ControllerBase
    {
        private readonly ICourseLibraryRepository _repo;
        private readonly IMapper _mapper;

        public AuthorsCollectionController(ICourseLibraryRepository repository, IMapper mapper)
        {
            _repo = repository ?? throw new NullReferenceException(nameof(repository));
            _mapper = mapper ?? throw new NullReferenceException(nameof(mapper));
        }

        [HttpPost]
        public ActionResult<IEnumerable<AuthorForCreationDTO>> CreateAuthorCollection
            (IEnumerable<AuthorForCreationDTO> authors)
        {
            var authorsEntity = _mapper.Map<IEnumerable<Author>>(authors);
            
            foreach (var author in authorsEntity)
            {
                _repo.AddAuthor(author);
            }
            _repo.Save();

            return Ok();
        }
    }
}
