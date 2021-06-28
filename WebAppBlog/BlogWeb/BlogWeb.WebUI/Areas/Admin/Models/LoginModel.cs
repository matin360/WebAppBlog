﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BlogWeb.WebUI.Areas.Admin.Models
{
	public class LoginModel
	{
		[Required]
		[EmailAddress]
		public string Email { get; set; }
		[Required]
		[StringLength(maximumLength: 20, MinimumLength = 6)]
		[DataType(DataType.Password)]
		public string Password { get; set; }
	}
}