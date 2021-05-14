﻿using BlogWeb.Domain.Concrete;
using BlogWeb.WebUI.Infrastructure;
using BlogWeb.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlogWeb.WebUI.Controllers
{
    public class HomeController : Controller
    {
        private BlogWebDbContext _dbContext;
        private int _itemsPerPage;

        public HomeController()
        {
            _dbContext = new BlogWebDbContext();
            _itemsPerPage = 6;
        }
        public ActionResult Index(PageModel model) => View(_dbContext.GetPaginatablePosts(_itemsPerPage, model));
       
    }
}