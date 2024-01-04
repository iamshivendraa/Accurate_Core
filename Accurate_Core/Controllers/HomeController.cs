using Accurate_Core.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Accurate_Core.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
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

        public IActionResult upload()
        {

            for (int i = 0; i < Request.Form.Files.Count; i++)
            {
                var file = Request.Form.Files[i];
                // Process and save the file to the database
                SaveFileToDatabase(file);
            }
            return Json(new { Message = "Files uploaded successfully." });
        }

        private void SaveFileToDatabase(IFormFile? file)
        {
            throw new NotImplementedException();
        }
    }
}
