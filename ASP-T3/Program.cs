using ASP_T3.Entities;
using System.Text.Json.Serialization;
namespace ASP_T3;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers()
            .AddJsonOptions(x =>
            x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

        var app = builder.Build();

        app.MapControllers();


        //app.MapGet("/", () => "Hello World!");

        //app.MapGet("/users", () =>
        //{
        //    using var context = new ApplicationDBContext();
        //    List<User> users = context.Users.ToList();

        //    return users;
        //});

        //app.MapGet("/users/{id}", (int id) =>
        //{
        //    using var context = new ApplicationDBContext();
        //    var user = (from u in context.Users
        //               where u.Id == id
        //               select u).FirstOrDefault();
        //    return user;
        //});

        //app.MapPost("/users", (User user) =>
        //{
        //    using var context = new ApplicationDBContext();
        //    //context.Add(user);
        //    context.Users.Add(user);
        //    int v = context.SaveChanges();

        //    return new { Message = "Inserted", Rows = v };
        //});

        //app.MapPut("/users", (User userReq) =>
        //{
        //    using var context = new ApplicationDBContext();
        //    var user = (from u in context.Users
        //                where u.Id == userReq.Id
        //                select u).FirstOrDefault();

        //    if (user != null)
        //    {
        //        user.Name = userReq.Name;
        //        user.Email = userReq.Email;


        //        int v = context.SaveChanges();
        //    }
        //    return "updated";
        //    //else
        //    //{
        //    //    return new { Message = "Updated" };
        //    //}
        //});

        //app.MapDelete("/users/{id}", (int id) =>
        //{
        //    using var context = new ApplicationDBContext();
        //    var user = (from u in context.Users
        //                where u.Id == id
        //                select u).FirstOrDefault();

        //    if (user != null)
        //    {
        //        context.Remove(user);
        //        context.SaveChanges();
        //        return "deleted";
        //    }
        //    return "not found";
        //    //return new { Message = "Inserted" };
        //});


        app.Run();
    }
}

