using Makale.Api.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Makale.Api.Services
{
    public interface ILibraryRepository
    {
        IEnumerable<Author> GetAuthors();
        Author GetAuthor(Guid authorId);
        IEnumerable<Author> GetAuthors(IEnumerable<Guid> authorIds);
        void AddAuthor(Author author);
        void DeleteAuthor(Author author);
        void UpdateAuthor(Author author);
        bool AuthorExists(Guid authorId);
        IEnumerable<Article> GetArticlesForAuthor(Guid authorId);
        Article GetArticleForAuthor(Guid authorId, Guid articleId);
        void AddArticleForAuthor(Guid authorId, Article article);
        void UpdateArticleForAuthor(Article article);
        void DeleteArticle(Article article);
        bool Save();
    }
}
