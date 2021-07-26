﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogWeb.Domain.Entities
{
	public class Post
	{
		public int Id { get; set; }
		[Required]
		[StringLength(maximumLength: 50)]
		public string Title { get; set; }
		[Required]
		[StringLength(maximumLength: 500)]
		public string ShortDescription { get; set; }
		[Required]
		[StringLength(maximumLength: 5000)]
		public string Text { get; set; }
		[Required]
		public byte[] ImageData { get; set; }
		[Required]
		public string ImageMimeType { get; set; }
		[Required]
		public int ViewsCount { get; set; }
		[Required]
		[DataType("smalldatetime")]
		public DateTime WrittenDate { get; set; }
		[Required]
		[DataType("smalldatetime")]
		public DateTime PublishDate { get; set; }
		public Author Author { get; set; }
		public int AuthorId { get; set; }
		public Category Category { get; set; }
		public int CategoryId { get; set; }
		public Archive Archive { get; set; }
		public int ArchiveId { get; set; }
		public ICollection<Comment> Comments { get; set; }
		public ICollection<Tag> Tags { get; set; }
		public Post()
		{
			Comments = new HashSet<Comment>();
			Tags = new HashSet<Tag>();
		}
	}
}
