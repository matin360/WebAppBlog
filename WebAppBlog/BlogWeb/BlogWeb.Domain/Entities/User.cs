using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogWeb.Domain.Entities
{
	public class User
	{
		public int Id { get; set; }
		[Required]
		[StringLength(maximumLength:50, ErrorMessage = "Maximum is 50 characters!")]
		public string Username { get; set; }
		[Required]
		[EmailAddress]
		[StringLength(maximumLength: 40, ErrorMessage = "Maximum is 50 characters!")]
		public string Email { get; set; }
		[Required]
		[StringLength(maximumLength: 20, MinimumLength = 6 ,ErrorMessage = "Maximum is 20 characters, Minimum is 6 characters")]
		public string Password { get; set; }
		[Required]
		[StringLength(maximumLength: 50, ErrorMessage = "Maximum is 50 characters!")]
		public int ImagePath { get; set; }
		[Required]
		public bool IsAuthor { get; set; }
		public ICollection<Post> Posts { get; set; }
		public ICollection<Comment> Comments { get; set; }
		public User()
		{
			Posts = new HashSet<Post>();
			Comments = new HashSet<Comment>();
		}
	}
}
