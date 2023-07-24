using System;
using ASP_T3.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;


using System.Text;

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
		public Object FindById(int id)
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

            string token = CreateToken(user);
            JwtSecurityToken jwtSecurityToken = new JwtSecurityTokenHandler().ReadJwtToken(token);
            Console.WriteLine(jwtSecurityToken);
            var a = VerifyJwt(token);
            Console.WriteLine(a);
            return new { Token = token, Verify = a };
        }

        private string CreateToken(User user) //JWT
        {

            // 1. Tao key để thực hiện ký trên jwt
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("Secret key abc1123 npcent"));

            // 2. List claims --> chính là phần payload
            List<Claim> claims = new List<Claim>
            {
                new Claim("UID", user.Id.ToString()),
                new Claim("Email", user.Email),
                new Claim(ClaimTypes.Name, user.Name),
                new Claim("host", "https://npcetc.com.vn"),
            };

            // 3. Tạo chữ ký
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            // 4. Tạo Token
            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds
               );

            // 5. Lấy token dưới dạng string
            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }

        private bool VerifyJwt(string jwt)
        {
            try
            {
                // 1. taoj key để xác thực
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("Secret key abc1123 npcent 111111"));
                //var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.Get("AppConfig.Secret"));

                // 
                SecurityToken stoken;

                // 2. sử dụng phương thức validateToken
                var jwtHandler = new JwtSecurityTokenHandler().ValidateToken(jwt, new TokenValidationParameters()
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = key,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                }, out stoken);

                var jwtToken = (JwtSecurityToken)stoken;
                var userName = jwtToken.Claims.First(x => x.Type == ClaimTypes.Name).Value;

                Console.WriteLine(userName);
                Console.WriteLine(jwtHandler.Claims.First());
                Console.WriteLine(stoken);
                return true;
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
            
        }
    }
}

