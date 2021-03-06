﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Makale.Api.Entities;
using Makale.Api.Helpers;
using Makale.Api.Models;
using Makale.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Makale.Api.Controller
{
    public class AuthorCollectionsController : ControllerBase
    {
        private ILibraryRepository _libraryRepository;

        public AuthorCollectionsController(ILibraryRepository libraryRepository)
        {
            _libraryRepository = libraryRepository;
        }

        [HttpPost]
        public IActionResult CreateAuthorCollection([FromBody] IEnumerable<AuthorForCreationDto> authorCollection)
        {
            if (authorCollection == null)
                return BadRequest();

            var authorEntities = Mapper.Map<IEnumerable<Author>>(authorCollection);

            foreach (var author in authorEntities)
            {
                _libraryRepository.AddAuthor(author);
            }

            if (!_libraryRepository.Save())
                throw new Exception("Createing an author collection failed on save");

            var authorCollectionToReturn = Mapper.Map<IEnumerable<AuthorDto>>(authorEntities);

            var idsAddString = string.Join(",",
                authorCollectionToReturn.Select(a => a.Id));

            return CreatedAtRoute("GetAuthorCollection",
                new { ids = idsAddString },
                authorCollectionToReturn);
        }

        [HttpGet("({ids})", Name = "GetAuthorCollection")]
        public IActionResult GetAuthorCollection([ModelBinder(BinderType = typeof(ArrayModelBinder))] IEnumerable<Guid> ids)
        {
            if (ids == null)
                return BadRequest();

            var authorEntities = _libraryRepository.GetAuthors(ids);
            if (ids.Count() != authorEntities.Count())
                return NotFound();

            var authorToReturn = Mapper.Map<IEnumerable<AuthorDto>>(authorEntities);
            return Ok(authorToReturn);

        }
    }
}