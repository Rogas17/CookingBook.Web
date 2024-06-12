using CookingBook.Web.Models;
using CookingBook.Web.Models.ViewModels;
using CookingBook.Web.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CookingBook.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IBlogPostRepository blogPostRepository;
        private readonly ITagRepository tagRepository;

        public HomeController(ILogger<HomeController> logger,
            IBlogPostRepository blogPostRepository,
            ITagRepository tagRepository
            )
        {
            _logger = logger;
            this.blogPostRepository = blogPostRepository;
            this.tagRepository = tagRepository;
        }

        public async Task<IActionResult> Index(string? searchQuery)
        {
            ViewBag.SearchQuery = searchQuery;

            //Get Wszystkie Blogi
            var posty = await blogPostRepository.GetAllAsync(searchQuery);

            //Get Wszystkie Tagi
            var tagi = await tagRepository.GetAllAsync(searchQuery);

            var model = new HomeViewModel
            {
                BlogPosts = posty,
                Tags = tagi
            };

            return View(model);
        }

        public IActionResult Privacy()
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
