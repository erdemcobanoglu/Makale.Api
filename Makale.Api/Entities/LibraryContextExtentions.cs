using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Makale.Api.Entities
{
    public static class LibraryContextExtentions
    {
        public static void EnsureSeedDataForContext(this LibraryContext context)
        {
            // first, clear the database.  // fresh each demo
            context.Authors.RemoveRange(context.Authors);
            context.SaveChanges();


            // seed data
            var authors = new List<Author>()
            {
               new Author()
               {
                    Id = new Guid("25320c5e-f58a-4b1f-b63a-8ee07a840bdf"),
                    FirstName = "FirstName_Author",
                    LastName = "LastName_Author",
                    Genre = "Horror",
                    DateOfBirth = new DateTimeOffset(new DateTime(1947, 9, 21)),
                    Articles = new List<Article>()
                     {
                         new Article()
                         {
                             Id = new Guid("c7ba6add-09c4-45f8-8dd0-eaca221e5d93"),
                             Title = "Lorem ipsum",
                             Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Praesent eu nisl in massa interdum condimentum. Duis tincidunt magna consectetur justo sagittis, vitae porta tortor volutpat."
                         },
                         new Article()
                         {
                             Id = new Guid("a3749477-f823-4124-aa4a-fc9ad5e79cd6"),
                             Title = "Misery",
                             Description = "Praesent tincidunt augue vel est dictum bibendum. In eget lectus felis. Donec metus tellus, congue at tortor ut, varius commodo tellus. Orci varius natoque penatibus et magnis dis parturient montes..s"
                         },
                         new Article()
                         {
                             Id = new Guid("70a1f9b9-0a37-4c1a-99b1-c7709fc64167"),
                             Title = "Fringilla",
                             Description = "urabitur eu efficitur elit. Nam magna nulla, vehicula ac mauris eget, scelerisque blandit arcu. Morbi eget tellus aliquam, elementum nisi a, semper est. Mauris ut leo dolor. Aenean aliquam tincidunt nisi."
                         },
                         new Article()
                         {
                             Id = new Guid("60188a2b-2784-4fc4-8df8-8919ff838b0b"),
                             Title = "The Curabitur",
                             Description = "Curabitur diam elit, malesuada vitae elit in, gravida tempus mi. Fusce id ornare mi, sed dapibus ligula. Integer ultrices tortor in ex aliquet, quis placerat orci blandit. In pulvinar nunc et ipsum ornare gravida. Maecenas sed pellentesque ipsum. "
                         }
                     }
               },
               new Author()
               {
                    Id = new Guid("76053df4-6687-4353-8937-b45556748abe"),
                    FirstName = "FirstName_Second_Author",
                    LastName = "LastName_Second_Author",
                    Genre = "Action",
                    DateOfBirth = new DateTimeOffset(new DateTime(1947, 9, 21)),
                    Articles = new List<Article>()
                     {
                         new Article()
                         {
                             Id = new Guid("447eb762-95e9-4c31-95e1-b20053fbe215"),
                             Title = "Integer",
                             Description = " Integer egestas sem turpis, eu gravida tortor scelerisque interdum. Cras in aliquam felis. Nunc mollis accumsan ornare. Etiam dui diam, ultricies vel ultrices et."
                         },
                         new Article()
                         {
                             Id = new Guid("bc4c35c3-3857-4250-9449-155fcf5109ec"),
                             Title = "Proin",
                             Description = "Proin odio nunc, accumsan accumsan euismod semper, iaculis at ante. Pellentesque dignissim orci urna, ut suscipit velit tincidunt quis. Quisque eget urna non elit dapibus mollis vitae in erat. Vivamus euismod ipsum nec felis dignissim, eget luctus ex efficitur. Nunc non scelerisque ligula.."
                         },
                         new Article()
                         {
                             Id = new Guid("09af5a52-9421-44e8-a2bb-a6b9ccbc8239"),
                             Title = "urabitur",
                             Description = "urabitur eu efficitur elit. Nam magna nulla, vehicula ac mauris eget, scelerisque blandit arcu. Morbi eget tellus aliquam, elementum nisi a, semper est."
                         }
                     }
               },
               new Author()
               {
                    Id = new Guid("412c3012-d891-4f5e-9613-ff7aa63e6bb3"),
                    FirstName = "FirstName_Guest_Author",
                    LastName = "LastName_Guest_Author",
                    Genre = "Comedy",
                    DateOfBirth = new DateTimeOffset(new DateTime(1947, 9, 21)),
                    Articles = new List<Article>()
                     {
                         new Article()
                         {
                             Id = new Guid("9edf91ee-ab77-4521-a402-5f188bc0c577"),
                             Title = "Aliquam",
                             Description = "  Aliquam non dolor hendrerit, molestie ex vel, auctor odio. Duis cursus, nibh et vestibulum mattis, mauris sem facilisis sem, et mollis dolor lorem at leo. Curabitur dignissim tempor risus at pretium."
                         },
                         new Article()
                         {
                             Id = new Guid("01457142-358f-495f-aafa-fb23de3d67e9"),
                             Title = "Pellentesque",
                             Description = "Pellentesque dignissim orci urna, ut suscipit velit tincidunt quis. Quisque eget urna non elit dapibus mollis vitae in erat. Vivamus euismod ipsum nec felis dignissim, eget luctus ex efficitur. Nunc non scelerisque ligula.."
                         }
                     }
               },
            };

            context.Authors.AddRange(authors);
            context.SaveChanges();
        }
    }
}
