namespace BlogWeb.Domain.Migrations
{
	using BlogWeb.Domain.Entities;
	using System;
	using System.Configuration;
	using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<BlogWeb.Domain.Concrete.BlogWebDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(BlogWeb.Domain.Concrete.BlogWebDbContext context)
        {
            context.Menus.AddOrUpdate(new Menu
            {
                Id = 1,
                Name = "Fashion",
                Action = "Index",
                Controller = "Fashion",
                IsActive = true
            },
            new Menu
            {
                Id = 2,
                Name = "Travel",
                Action = "Index",
                Controller = "Travel",
                IsActive = true
            },
            new Menu
            {
                Id = 3,
                Name = "About",
                Action = "Index",
                Controller = "About",
                IsActive = true
            },
            new Menu
            {
                Id = 4,
                Name = "Contact",
                Action = "Index",
                Controller = "Contact",
                IsActive = true
            }
            );
            context.Categories.AddOrUpdate(new Category
            {
                Id = 1,
                Name = "Fashion"
            },
            new Category
            {
                Id = 2,
                Name = "Technology"
            },
            new Category
            {
                Id = 3,
                Name = "Travel"
            },
            new Category
            {
                Id = 2,
                Name = "Food"
            },
            new Category
            {
                Id = 3,
                Name = "Photography"
            }
            );

            context.Tags.AddOrUpdate(new Tag
            {
                Id = 1,
                Name = "Animals"
            },
            new Tag
            {
                Id = 2,
                Name = "Human"
            },
            new Tag
            {
                Id = 3,
                Name = "People"
            },
            new Tag
            {
                Id = 4,
                Name = "Cat"
            },
            new Tag
            {
                Id = 5,
                Name = "Dog"
            },
            new Tag
            {
                Id = 6,
                Name = "Nature"
            },
            new Tag
             {
                 Id = 7,
                 Name = "Leaves"
             },
            new Tag
            {
                Id = 8,
                Name = "Food"
            }
            );

            context.Archives.AddOrUpdate(new Archive
            {
                Id = 1,
                Month = "December",
                Year = "2018"
            },
            new Archive
            {
                Id = 2,
                Month = "September",
                Year = "2018"
            },
            new Archive
            {
                Id = 3,
                Month = "August",
                Year = "2018"
            },
            new Archive
            {
                Id = 4,
                Month = "July",
                Year = "2018"
            },
            new Archive
            {
                Id = 5,
                Month = "June",
                Year = "2018"
            },
            new Archive
            {
                Id = 6,
                Month = "May",
                Year = "2018"
            }
            );

			context.Users.AddOrUpdate(new User
			{
				Id = 1,
				IsAuthor = true,
				Email = ConfigurationManager.AppSettings["email"],
				Password = ConfigurationManager.AppSettings["password"],
				Username = "Jhon Smith",
				ImagePath = "person_1.png"
			}
			);

			context.Authors.AddOrUpdate(new Author
			{
				Id = 1,
				UserId = 1,
				Description = "John Smith (baptized 6 January 1580 – 21 June 1631) was an English soldier, explorer, colonial governor, Admiral of New England, and author. He played an important role in the establishment of the colony at Jamestown"
			});


		}
    }
}
