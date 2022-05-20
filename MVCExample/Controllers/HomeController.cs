using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AnilTest;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MVCExample.Models;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Http.Features;
using MVCExample.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace MVCExample.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;


        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult GetIp()
        {
            ViewBag.ipaddress = HttpContext.Connection.RemoteIpAddress.ToString();

            return View("Index");
        }


        public IActionResult Index()
        {
            var model = new Book();
            var bookList = default(List<Book>);

            using (AppDbContext dbContext = new AppDbContext())
            {
                var data = dbContext.Student.FirstOrDefault();
                bookList = dbContext.Book.Select(x => new Book()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Category = x.Category,
                    Author = x.Author,
                    Price = x.Price,
                    Year = x.Year

                }).ToList();
                }

            return View(bookList);
        }

    
        /*[HttpPost]
        public IActionResult Index(Book book)
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            var config = builder.Build();
            string constring = config.GetConnectionString("con");
            

            var id = book.Id;
            var name = book.Name;
            var category = book.Category;
            var author = book.Author;
            var price = book.Price;
            var year = book.Year;

            return View(new Book());
        }*/
        

        public IActionResult Student()
        {
            var model = new Book();
            var studentList = default(List<Student>);

            using (AppDbContext dbContext = new AppDbContext())
            {
                var data = dbContext.Student.FirstOrDefault(); //en başta veri geliyormu diye debug ederken görmek için yazdım.
                studentList = dbContext.Student.Select(x => new Student()
                {
                    Id = x.Id,
                    stuName = x.stuName,
                    stuSurname = x.stuSurname,
                    studentId = x.studentId,
                    stuMail = x.stuMail,
                    stuPhoneNumber = x.stuPhoneNumber
                   

                }).ToList();
            }

            return View(studentList);
            
        }

        public IActionResult AddNewStudent()
        {
            AddNewStudent();
            return View();
        }

        public IActionResult AddBook()
        {

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }

}
