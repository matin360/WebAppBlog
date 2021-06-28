using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlogWeb.WebUI.Areas.Admin.Filters
{
	[System.AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
	sealed class AuthorizeFilter : FilterAttribute, IAuthorizationFilter
	{
		private readonly string _url;
		private readonly string _key;
		public AuthorizeFilter(string url, string key)
		{
			_key = key;
			_url = url;
		}

		public void OnAuthorization(AuthorizationContext context)
		{
			if (context.HttpContext.Session[_key] == null)
				context.HttpContext.Response.Redirect(_url);
		}
	}
}