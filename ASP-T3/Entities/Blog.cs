using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ASP_T3.Entities
{
	[Table("tblBlogs")]
	public class Blog
	{
		public Blog()
		{
		}

		[Key]
		public int Id { get; set; }
		public string Url { get; set; }
		public string Title { get; set; }
		public int UserId { get; set; } // foreign key
		public User User { get; set; } // navigation prop

        public Blog(string url, string title, int userId, User user)
        {
            Url = url;
            Title = title;
            UserId = userId;
			User = user;
        }
    }
}

