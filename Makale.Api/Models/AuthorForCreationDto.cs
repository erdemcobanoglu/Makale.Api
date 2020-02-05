using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Makale.Api.Models
{
    public class AuthorForCreationDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTimeOffset DateOfBirth { get; set; }
        public string Genre { get; set; }

        public ICollection<ArticleForCreationDto> Articles { get; set; }
        = new List<ArticleForCreationDto>();
    }
}
