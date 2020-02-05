using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Makale.Api.Models
{
    public class ArticleForUpdateDto : ArticleForManipulationDto
    {
        public override string Description
        {
            get { return base.Description; }
            set { base.Description = value; }
        }
    }
}
