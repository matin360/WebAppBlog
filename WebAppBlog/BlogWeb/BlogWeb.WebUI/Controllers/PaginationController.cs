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
    public class PaginationController : Controller
    {
        private BlogWebDbContext _dbContext;

        public PaginationController()
        {
            _dbContext = new BlogWebDbContext();
        }
        public ActionResult Pages(PageModel model)
        {
            return View(_dbContext.GetPages(model));
        }
    }
}