using BlogWeb.Domain.Concrete;
using BlogWeb.Domain.Entities;
using BlogWeb.WebUI.Areas.Admin.Models;
using BlogWeb.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace BlogWeb.WebUI.Infrastructure
{
	public static class DbContextExtensions
	{
		public static IEnumerable<CategoryViewModel> GetAllCategories(this BlogWebDbContext _dbContext)
		{
			return _dbContext.Categories.Select(x => new CategoryViewModel
			{
				Name = x.Name,
				PostsCount = x.Posts.Count()
			}).ToList();
		}

		public static IEnumerable<ArchiveViewModel> GetAllArchives(this BlogWebDbContext _dbContext)
		{
			return _dbContext.Archives.Select(x => new ArchiveViewModel
			{
				Month = x.Month,
				Year = x.Year,
				PostsCount = x.Posts.Count()
			}).ToList();
		}

		public static IEnumerable<TagViewModel> GetAllTags(this BlogWebDbContext _dbContext)
		{
			return _dbContext.Tags.Select(x => new TagViewModel
			{
				Name = x.Name
			}).ToList();
		}

		public static async Task<IEnumerable<PostViewModel>> GetPaginatablePostsAsync(this BlogWebDbContext _dbContext, int itemsPerPage, PageModel model)
		{
			return await _dbContext.Posts.OrderBy( x => x.WrittenDate)
				.Skip((itemsPerPage * model.Number) - itemsPerPage).Take(itemsPerPage)
				.Select(x => new PostViewModel
			{
				Id = x.Id,
				Title = x.Title,
				ShortDescription = x.ShortDescription,
				WrittenDate = x.WrittenDate,
				CommentsCount = x.Comments.Count,
				CategoryName = x.Category.Name,
				ImageData = x.ImageData,
				ImageMimeType = x.ImageMimeType
			}).ToListAsync();
		}

		public static async Task<IEnumerable<PostTravelViewModel>> GetPaginatablePostTravelsAsync(this BlogWebDbContext _dbContext, int itemsPerPage, PageModel model)
		{
			return await _dbContext.Posts.OrderBy(x => x.WrittenDate)
				.Skip((itemsPerPage * model.Number) - itemsPerPage).Take(itemsPerPage)
				.Select(x => new PostTravelViewModel
				{
					Id = x.Id,
					Title = x.Title,
					ShortDescription = x.ShortDescription,
					WrittenDate = x.WrittenDate,
					CommentsCount = x.Comments.Count,
					ImageData = x.ImageData,
					ImageMimeType = x.ImageMimeType,
					Author = _dbContext.Authors.Where(y => y.Id == x.AuthorId)
										.Select(y => new AuthorViewModel
										{
											Description = y.Description,
											ImagePath = y.User.ImagePath,
											Username = y.User.Username
										}).FirstOrDefault()
		}).ToListAsync();
		}

		public static PageModel GetPages(this BlogWebDbContext _dbContext, PageModel model)
		{
			int postsCount = _dbContext.Posts.Count();
			int pagesCount = postsCount % 6 == 0 ? postsCount / 6 : postsCount / 6 + 1;
			model.PagesCount = pagesCount;
			return model;
		}

		public static IEnumerable<PopularPostViewModel> GetPopularPosts(this BlogWebDbContext _dbContext)
		{
			return _dbContext.Posts.OrderByDescending(x => x.ViewsCount).Take(3)
				.Select(x => new PopularPostViewModel
				{
					Id = x.Id,
					Title = x.Title,
					PublishDate = x.PublishDate,
					AuthorName = x.Author.User.Username,
					CommnetsCount = x.Comments.Count,
					ImageData = x.ImageData,
					ImageMimeType = x.ImageMimeType
				}).ToList();
		}

		public static async Task<PostDetailsViewModel> GetSinglePostDetailsAsync(this BlogWebDbContext _dbContext, int id)
		{
			var post = await _dbContext.Posts.FindAsync(id);

			var postFull = new PostDetailsViewModel
			{
				Id = post.Id,
				ShortDescription = post.ShortDescription,
				ImageData = post.ImageData,
				ImageMimeType = post.ImageMimeType,
				Text = post.Text,
				Title = post.Title,
				ViewsCount = post.ViewsCount
			};

			postFull.CategoryName = _dbContext.Categories.Where(x => x.Id == post.CategoryId).
													Select(x => new CategoryViewModel
													{
														Name = x.Name
													}).FirstOrDefaultAsync().GetAwaiter().GetResult().Name;
													

			postFull.Author = await _dbContext.Authors.Where(x => x.Id == post.AuthorId)
										.Select(x => new AuthorViewModel
										{
											Description = x.Description,
											ImagePath = x.User.ImagePath,
											Username = x.User.Username
										}).FirstOrDefaultAsync();

			postFull.Comments = await _dbContext.Comments.Where(x => x.PostId == post.Id).
													Select(x => new CommentViewModel
													{
														Message = x.Message,
														SubmmittedDate = x.SubmmittedDate,
														User = x.User,
														Username = x.Username
													}).ToListAsync();


			postFull.Tags = post.Tags.
				Select(x => new TagViewModel
				{
					Name = x.Name
				}).ToList();

			return postFull;
		}

		public static async Task<int>  AddCommentAsync(this BlogWebDbContext _dbContext, CommentPostModel model)
		{
			Comment comment = new Comment
			{
				Email = model.Email,
				SubmmittedDate = DateTime.Now,
				Message = model.Message,
				PostId = model.PostId,
				Username = model.Username,
				Website = model.Website,
				UserId = 1
			};
			_dbContext.Comments.Add(comment);
			return await _dbContext.SaveChangesAsync();
		}
		public static async Task<int> SendMessageAsync(this BlogWebDbContext _dbContext, ContactMessageViewModel model)
		{
			ContactMessage message = new ContactMessage
			{
				Email = model.Email,
				SubmmittedDate = DateTime.Now,
				Message = model.Message,
				Name = model.Name,
				Subject = model.Subject
			};
			_dbContext.ContactMessages.Add(message);
			return await _dbContext.SaveChangesAsync();
		}

		public static IEnumerable<MenuViewModel> GetAllMenus(this BlogWebDbContext _dbContext)
		{
			return _dbContext.Menus.Where( x => x.IsActive)
					.Select(x => new MenuViewModel
			{
				Name = x.Name,
				Controller = x.Controller,
				Action = x.Action
			}).ToList();
		}

		public static async Task<User> GetUserAsync(this BlogWebDbContext _dbContext, LoginModel model)
		{
			return await _dbContext.Users.Where(x => x.Email == model.Email && x.Password == model.Password).FirstOrDefaultAsync();
		}

		public static async Task<IEnumerable<EntityReportModel>> GetEntitiesCountAsync(this BlogWebDbContext _dbContext)
		{
			// id, name, count
			List<EntityReportModel> reportModels = new List<EntityReportModel>();

			EntityReportModel contacts = new EntityReportModel
			{
				Name = "Contact Messages",
				Count = await _dbContext.ContactMessages.CountAsync()
			};
			EntityReportModel archives = new EntityReportModel
			{
				Name = "Archives",
				Count = await _dbContext.Archives.CountAsync()
			};
			EntityReportModel posts = new EntityReportModel
			{
				Name = "Posts",
				Count = await _dbContext.Posts.CountAsync()
			};
			EntityReportModel users = new EntityReportModel
			{
				Name = "Users",
				Count = await _dbContext.Users.CountAsync()
			};
			EntityReportModel tags = new EntityReportModel
			{
				Name = "Tags",
				Count = await _dbContext.Tags.CountAsync()
			};
			EntityReportModel authors = new EntityReportModel
			{
				Name = "Authors",
				Count = await _dbContext.Authors.CountAsync()
			};
			EntityReportModel comments = new EntityReportModel
			{
				Name = "Comments",
				Count = await _dbContext.Comments.CountAsync()
			};
			EntityReportModel categs = new EntityReportModel
			{
				Name = "Categories",
				Count = await _dbContext.Categories.CountAsync()
			};

			reportModels.Add(posts);
			reportModels.Add(users);
			reportModels.Add(tags);
			reportModels.Add(categs);
			reportModels.Add(authors);
			reportModels.Add(comments);
			reportModels.Add(contacts);
			reportModels.Add(archives);
			return reportModels;
		}

		public static async Task<IEnumerable<PostReportViewModel>> GetAllPostsAsync(this BlogWebDbContext _dbContext)
		{
			return await _dbContext.Posts.OrderBy(x => x.WrittenDate)
				.Select(x => new PostReportViewModel
				{
					Id = x.Id,
					Title = x.Title,
					ShortDescription = x.ShortDescription,
					WrittenDate = x.WrittenDate,
					PublishedDate = x.PublishDate,
					CommentsCount = x.Comments.Count,
					CategoryName = _dbContext.Categories.Where(y => y.Id == x.CategoryId).FirstOrDefault().Name,
					Author = _dbContext.Authors.Where(y => y.Id == x.AuthorId)
										.Select(y => new AuthorViewModel
										{
											Description = y.Description,
											ImagePath = y.User.ImagePath,
											Username = y.User.Username
										}).FirstOrDefault()
				}).ToListAsync();
		}

		public static async Task<PostEditModel> GetPostEditModelAsync(this BlogWebDbContext _dbContext, int id)
		{
			var post = await _dbContext.Posts.FindAsync(id);

			if(post != null)
			{
				return new PostEditModel();
			}
			else
			{
				var postFull = new PostEditModel
				{
					Id = post.Id,
					ShortDescription = post.ShortDescription,
					Title = post.Title,
					Text = post.Text,
					ImageData = post.ImageData,
					ImageMimeType = post.ImageMimeType,
					WrittenDate = post.WrittenDate,
					AuthorId = post.AuthorId,
					CategoryId = post.CategoryId
				};

				postFull.CategoryName = _dbContext.Categories.Where(x => x.Id == post.CategoryId).
														Select(x => new CategoryViewModel
														{
															Name = x.Name
														}).FirstOrDefaultAsync().GetAwaiter().GetResult().Name;


				postFull.Author = await _dbContext.Authors.Where(x => x.Id == post.AuthorId)
											.Select(x => new AuthorViewModel
											{
												Description = x.Description,
												ImagePath = x.User.ImagePath,
												Username = x.User.Username
											}).FirstOrDefaultAsync();

				postFull.Categories = await _dbContext.Categories.Select(x => new SelectListItem
				{
					Value = x.Name,
					Text = x.Name
				}).ToListAsync();

				return postFull;
			}
		}

		public static async Task<Post> GetPostAsync(this BlogWebDbContext _dbContext, int id)
		{
			return await _dbContext.Posts.FindAsync(id);
		}

		//SavePostAsync - Add and Edit
		public static async Task<int> SavePostAsync(this BlogWebDbContext _dbContext, PostEditModel model)
		{
			var category = await _dbContext.Categories.Where(x => x.Name == model.CategoryName).FirstOrDefaultAsync();
			Post post = new Post
			{
				AuthorId = model.AuthorId,
				CategoryId = model.CategoryId,
				ImageData = model.ImageData,
				ImageMimeType = model.ImageMimeType,
				ShortDescription = model.ShortDescription,
				Text = model.Text,
				PublishDate = model.WrittenDate,
				WrittenDate = model.WrittenDate,
				Title = model.Title,
				Category = category,
				ArchiveId = 1
			};

			var existingPost = await _dbContext.Posts.FindAsync(model.Id);


			if(existingPost == null)
			{
				_dbContext.Posts.Add(post);
			}
			else
			{
				//Post dbEntry = await _dbContext.Posts.FindAsync()
				existingPost.Title = model.Title;
				existingPost.CategoryId = model.CategoryId;
				existingPost.ImageData = model.ImageData ?? existingPost.ImageData;
				existingPost.ImageMimeType = model.ImageMimeType ?? existingPost.ImageMimeType;
				existingPost.ShortDescription = model.ShortDescription;
				existingPost.Text = model.Text;
				existingPost.PublishDate = model.WrittenDate;
				existingPost.WrittenDate = model.WrittenDate;
			}


			_dbContext.Posts.Add(post);
			return await _dbContext.SaveChangesAsync();
		}
	}
}
