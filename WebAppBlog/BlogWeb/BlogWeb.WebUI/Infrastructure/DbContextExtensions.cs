using BlogWeb.Domain.Concrete;
using BlogWeb.Domain.Entities;
using BlogWeb.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

		public static IEnumerable<PostViewModel> GetPaginatablePosts(this BlogWebDbContext _dbContext, int itemsPerPage, PageModel model)
		{
			return _dbContext.Posts.OrderBy( x => x.WrittenDate)
				.Skip((itemsPerPage * model.Number) - itemsPerPage).Take(itemsPerPage)
				.Select(x => new PostViewModel
			{
				Id = x.Id,
				Title = x.Title,
				ShortDescription = x.ShortDescription,
				WrittenDate = x.WrittenDate,
				CommentsCount = x.Comments.Count,
				CategoryName = x.Category.Name,
				ImagePath = x.ImagePath
			}).ToList();
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
					Title = x.Title,
					PublishDate = x.PublishDate,
					AuthorName = x.Author.User.Username,
					CommnetsCount = x.Comments.Count,
					ImagePath = x.ImagePath
				}).ToList();
		}

		public static PostDetailsViewModel GetSinglePostDetails(this BlogWebDbContext _dbContext, int id)
		{
			var post = _dbContext.Posts.Find(id);

			var postFull = new PostDetailsViewModel
			{
				Id = post.Id,
				ShortDescription = post.ShortDescription,
				ImagePath = post.ImagePath,
				Text = post.Text,
				Title = post.Title,
				ViewsCount = post.ViewsCount
			};

			postFull.CategoryName = _dbContext.Categories.Where(x => x.Id == post.CategoryId).
													Select(x => new CategoryViewModel
													{
														Name = x.Name
													}).FirstOrDefault().Name;
													

			postFull.Author = _dbContext.Authors.Where(x => x.Id == post.AuthorId)
										.Select(x => new AuthorViewModel
										{
											Description = x.Description,
											ImagePath = x.User.ImagePath,
											Username = x.User.Username
										}).FirstOrDefault();

			postFull.Comments = _dbContext.Comments.Where(x => x.PostId == post.Id).
													Select(x => new CommentViewModel
													{
														Message = x.Message,
														SubmmittedDate = x.SubmmittedDate,
														User =x.User,
														Username = x.Username
													});

			postFull.Tags = post.Tags.
				Select(x => new TagViewModel
				{
					Name = x.Name
				}).ToList();

			return postFull;
		}
	}
}
