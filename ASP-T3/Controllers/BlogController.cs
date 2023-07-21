using System;
using ASP_T3.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ASP_T3.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BlogController
	{

        [HttpGet]
        public List<Blog> List()
        {
            using var context = new ApplicationDBContext();
            return context.Blogs
                .Include("User")
                //.Include(b => b.User)
                .ToList();
        }

	}
}

