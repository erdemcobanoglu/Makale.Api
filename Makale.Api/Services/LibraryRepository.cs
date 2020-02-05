using Makale.Api.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Makale.Api.Services
{ 
    public class LibraryRepository : ILibraryRepository
    {
        private LibraryContext _context;
        public LibraryRepository(LibraryContext context)
        {
            _context = context;
        }
        public void AddAuthor(Author author)
        {
            author.Id = Guid.NewGuid();
            _context.Authors.Add(author);

            // the repository fills the id (instead of using identity columns)
            if (author.Articles.Any())
            {
                foreach (var article in author.Articles)
                {
                    article.Id = Guid.NewGuid();
                }
            }

        }

        public void AddArticleForAuthor(Guid authorId, Article article)
        {
            var author = GetAuthor(authorId);
            if (author != null)
            {
                if (article.Id == Guid.Empty)
                {
                    article.Id = Guid.NewGuid();
                }
                author.Articles.Add(article);
            }
        } 
        public Author GetAuthor(Guid authorId)
        {
            return _context.Authors.FirstOrDefault(a => a.Id == authorId);
        }
        public bool AuthorExists(Guid authorId)
        {
            return _context.Authors.Any(a => a.Id == authorId);
        }
        public void DeleteAuthor(Author author)
        {
            _context.Authors.Remove(author);
        }
        public void DeleteArticle(Article article)
        {
            _context.Articles.Remove(article);
        }
        public IEnumerable<Author> GetAuthors()
        {
            return _context.Authors
                .OrderBy(a => a.FirstName)
                .ThenBy(a => a.LastName)
                .ToList();
        }
        public IEnumerable<Author> GetAuthors(IEnumerable<Guid> authorIds)
        {
            return _context.Authors.Where(a => authorIds.Contains(a.Id))
                .OrderBy(a => a.FirstName)
                .OrderBy(a => a.LastName)
                .ToList();
        }
        public void UpdateAuthor(Author author)
        {
            // no code in this İmplementation
        }

        public Article GetArticleForAuthor(Guid authorId, Guid articleId)
        {
            return _context.Articles
                .Where(a => a.AuthorId == authorId && a.Id == articleId).FirstOrDefault();
        }
        public IEnumerable<Article> GetArticlesForAuthor(Guid authorId)
        {
            return _context.Articles
                .Where(a => a.AuthorId == authorId).OrderBy(a => a.Title).ToList();
        }

        public void UpdateArticleForAuthor(Article article)
        {
            // no code in this İmplementation
        }

        public bool Save()
        {
            return (_context.SaveChanges() >= 0);
        }

    }
}
