using BlogWeb.Domain.Concrete;
using BlogWeb.WebUI.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlogWeb.WebUI.Controllers
{
    public class PostController : Controller
    {
        private BlogWebDbContext _dbContext;

        public PostController()
        {
            _dbContext = new BlogWebDbContext();
        }
        public ActionResult AllPopular()
        {
            return View(_dbContext.GetPopularPosts());
        }
        public ActionResult Details(int id)
		{
            return View(_dbContext.GetSinglePostDetails(id));
        }
    }
}