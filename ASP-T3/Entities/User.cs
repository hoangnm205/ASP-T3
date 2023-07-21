using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ASP_T3.Entities
{
	[Table("tblUsers")]
	public class User
	{
		[Key]
		public int Id { get; set; }
		public string Name { get; set; }
		public string Email { get; set; }

        [MinLength(6)]
        public string Password { get; set; }

        public List<Blog> Blogs { get; } = new();

		public User()
		{
		}

        public User(int id, string name, string email, string password)
        {
            Id = id;
            Name = name;
            Email = email;
            Password = password;
        }
    }
}

