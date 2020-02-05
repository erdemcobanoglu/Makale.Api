using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Makale.Api.Entities;
using Makale.Api.Models;
using Makale.Api.Services;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Makale.Api.Controller
{

    [Route("api/authors/{authorId}/articles")]
    public class ArticlesController : ControllerBase
    {
        private ILibraryRepository _libraryRepository;
        private ILogger<ArticlesController> _logger;

        public ArticlesController(ILibraryRepository libraryRepository,
            ILogger<ArticlesController> logger)
        {
            _logger = logger;
            _libraryRepository = libraryRepository;
        }

        [HttpGet()]
        public IActionResult GetArticlesForAuthor(Guid authorId)
        {
            if (!_libraryRepository.AuthorExists(authorId))
            {
                return NotFound();
            }

            var articlesForAuthorFromRepo = _libraryRepository.GetArticlesForAuthor(authorId);

            var articlesForAuthor = Mapper.Map<IEnumerable<ArticleDto>>(articlesForAuthorFromRepo);

            return Ok(articlesForAuthor);
        }

        [HttpGet("{id}", Name = "GetArticleForAuthor")]
        public IActionResult GetArticleForAuthor(Guid authorId, Guid id)
        {
            if (!_libraryRepository.AuthorExists(authorId))
            {
                return NotFound();
            }

            var articleForAuthorFromRepo = _libraryRepository.GetArticleForAuthor(authorId, id);
            if (articleForAuthorFromRepo == null)
            {
                return NotFound();
            }

            var articleForAuthor = Mapper.Map<ArticleDto>(articleForAuthorFromRepo);
            return Ok(articleForAuthor);
        }

        [HttpPost()]
        public IActionResult CreateArticleForAuthor(Guid authorId,
            [FromBody] ArticleForCreationDto article)
        {
            if (article == null)
            {
                return BadRequest();
            }

            if (article.Description == article.Title)
            {
                ModelState.AddModelError(nameof(ArticleForCreationDto),
                    "The provided description should be different from the title.");
            }

            if (!ModelState.IsValid)
            {
                // return 422
                return new UnprocessableEntityObjectResult(ModelState);
            }

            if (!_libraryRepository.AuthorExists(authorId))
            {
                return NotFound();
            }

            var articleEntity = Mapper.Map<Article>(article);

            _libraryRepository.AddArticleForAuthor(authorId, articleEntity);

            if (!_libraryRepository.Save())
            {
                throw new Exception($"Creating a article for author {authorId} failed on save.");
            }

            var articleToReturn = Mapper.Map<ArticleDto>(articleEntity);

            return CreatedAtRoute("GetArticleForAuthor",
                new { authorId = authorId, id = articleToReturn.Id },
                articleToReturn);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteArticleForAuthor(Guid authorId, Guid id)
        {
            if (!_libraryRepository.AuthorExists(authorId))
            {
                return NotFound();
            }

            var articleForAuthorFromRepo = _libraryRepository.GetArticleForAuthor(authorId, id);
            if (articleForAuthorFromRepo == null)
            {
                return NotFound();
            }

            _libraryRepository.DeleteArticle(articleForAuthorFromRepo);

            if (!_libraryRepository.Save())
            {
                throw new Exception($"Deleting article {id} for author {authorId} failed on save.");
            }

            _logger.LogInformation(100, $"Article {id} for author {authorId} was deleted.");

            return NoContent();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateArticleForAuthor(Guid authorId, Guid id,
            [FromBody] ArticleForUpdateDto article)
        {
            if (article == null)
            {
                return BadRequest();
            }

            if (article.Description == article.Title)
            {
                ModelState.AddModelError(nameof(ArticleForUpdateDto),
                    "The provided description should be different from the title.");
            }

            if (!ModelState.IsValid)
            {
                return new UnprocessableEntityObjectResult(ModelState);
            }


            if (!_libraryRepository.AuthorExists(authorId))
            {
                return NotFound();
            }

            var articleForAuthorFromRepo = _libraryRepository.GetArticleForAuthor(authorId, id);
            if (articleForAuthorFromRepo == null)
            {
                var articleToAdd = Mapper.Map<Article>(article);
                articleToAdd.Id = id;

                _libraryRepository.AddArticleForAuthor(authorId, articleToAdd);

                if (!_libraryRepository.Save())
                {
                    throw new Exception($"Upserting article {id} for author {authorId} failed on save.");
                }

                var articleToReturn = Mapper.Map<ArticleDto>(articleToAdd);

                return CreatedAtRoute("GetArticleForAuthor",
                    new { authorId = authorId, id = articleToReturn.Id },
                    articleToReturn);
            }

            Mapper.Map(article, articleForAuthorFromRepo);

            _libraryRepository.UpdateArticleForAuthor(articleForAuthorFromRepo);

            if (!_libraryRepository.Save())
            {
                throw new Exception($"Updating article {id} for author {authorId} failed on save.");
            }

            return NoContent();
        }

        [HttpPatch("{id}")]
        public IActionResult PartiallyUpdateArticleForAuthor(Guid authorId, Guid id,
            [FromBody] JsonPatchDocument<ArticleForUpdateDto> patchDoc)
        {
            if (patchDoc == null)
            {
                return BadRequest();
            }

            if (!_libraryRepository.AuthorExists(authorId))
            {
                return NotFound();
            }

            var articleForAuthorFromRepo = _libraryRepository.GetArticleForAuthor(authorId, id);

            if (articleForAuthorFromRepo == null)
            {
                var articleDto = new ArticleForUpdateDto();
                patchDoc.ApplyTo(articleDto, ModelState);

                if (articleDto.Description == articleDto.Title)
                {
                    ModelState.AddModelError(nameof(ArticleForUpdateDto),
                        "The provided description should be different from the title.");
                }

                TryValidateModel(articleDto);

                if (!ModelState.IsValid)
                {
                    return new UnprocessableEntityObjectResult(ModelState);
                }

                var articleToAdd = Mapper.Map<Article>(articleDto);
                articleToAdd.Id = id;

                _libraryRepository.AddArticleForAuthor(authorId, articleToAdd);

                if (!_libraryRepository.Save())
                {
                    throw new Exception($"Upserting article {id} for author {authorId} failed on save.");
                }

                var articleToReturn = Mapper.Map<ArticleDto>(articleToAdd);
                return CreatedAtRoute("GetArticleForAuthor",
                    new { authorId = authorId, id = articleToReturn.Id },
                    articleToReturn);
            }

            var articleToPatch = Mapper.Map<ArticleForUpdateDto>(articleForAuthorFromRepo);

            patchDoc.ApplyTo(articleToPatch, ModelState);

             

            if (articleToPatch.Description == articleToPatch.Title)
            {
                ModelState.AddModelError(nameof(ArticleForUpdateDto),
                    "The provided description should be different from the title.");
            }

            TryValidateModel(articleToPatch);

            if (!ModelState.IsValid)
            {
                return new UnprocessableEntityObjectResult(ModelState);
            }

            Mapper.Map(articleToPatch, articleForAuthorFromRepo);

            _libraryRepository.UpdateArticleForAuthor(articleForAuthorFromRepo);

            if (!_libraryRepository.Save())
            {
                throw new Exception($"Patching article {id} for author {authorId} failed on save.");
            }

            return NoContent();
        }
    }
}