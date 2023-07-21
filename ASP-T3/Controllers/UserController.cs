using System;
using ASP_T3.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ASP_T3.Controllers
{
    [ApiController]
    [Route("users")] // Path = "/users"
	public class UserController : ControllerBase
	{
		[HttpGet] // Path = "/"
		public List<User> GetUsers()
		{
            using var context = new ApplicationDBContext();
            return context.Users.ToList();
        }

        [HttpPost]
        public Object Create(User u)
		{
            using var context = new ApplicationDBContext();
            context.Add(u);
            int v = context.SaveChanges();

            return new { Message = "Inserted", Rows = v };
        }

        [HttpPut("{id}")]
        public Object Update(int id, User userReq)
        {
            using var context = new ApplicationDBContext();
            var user = (from u in context.Users
                        where u.Id == userReq.Id
                        select u).FirstOrDefault();

            if (user != null)
            {
                user.Name = userReq.Name;
                user.Email = userReq.Email;


                int v = context.SaveChanges();
                return new { Message = "Updated" };
            }
            return new { Message = "Not found" };
        }

        [HttpDelete("{id}")]
        public Object Delete(int id)
        {
            using var context = new ApplicationDBContext();
            var user = (from u in context.Users
                        where u.Id == id
                        select u).FirstOrDefault();

            if (user != null)
            {
                context.Remove(user);
                context.SaveChanges();
                return new { Message = "Deleted" };
            }
            return new { Message = "Not found" };
        }

        [HttpGet("{id}")]
		public User? FindById(int id)
		{
            using var context = new ApplicationDBContext();
            var user = (from u in context.Users.Include("Blogs")
                    where u.Id == id
                    select u).FirstOrDefault();


            //foreach (var b in user.Blogs)
            //{
            //    Console.WriteLine(b.Title);
            //}

            //user.Blogs.Add(new Blog("test", "test", user.Id, user));
            //context.SaveChanges();

            return user;
        }
    }
}

