using Accurate_Core.App_Data;
using Accurate_Core.Models;
using ExcelDataReader;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore; // Add this namespace for Entity Framework
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Accurate_Core.Controllers
{
    public class OrderController : Controller
    {
        private readonly ApplicationDbContext _db;

        public OrderController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public IActionResult Index(List<ExcelSample> excelData = null)
        {
            excelData = excelData == null ? new List<ExcelSample>() : excelData;

            // Retrieve data from the database
            var dbData = _db.ExcelData.ToList();
            // Pass both Excel data and database data to the view
            var viewModel = new OrderViewModel { ExcelData = excelData, DbData = dbData };
            return View(viewModel);
        }


        [HttpPost]
        public IActionResult Index(IFormFile file, [FromServices] Microsoft.AspNetCore.Hosting.IWebHostEnvironment hostingEnvironment)
        {
            string fileName = $"{hostingEnvironment.WebRootPath}\\files\\{file.FileName}";

            using (FileStream fileStream = System.IO.File.Create(fileName))
            {
                file.CopyTo(fileStream);
                fileStream.Flush();
            }

            var excelData = GetExcelDataList(file.FileName);

            // Store data in the database directly
            foreach (var data in excelData)
            {
                _db.ExcelData.Add(data);
            }

            _db.SaveChanges();

            // Retrieve data from the database
            var dbData = _db.ExcelData.ToList();

            // Pass both Excel data and database data to the view
            return View(new OrderViewModel { ExcelData = excelData, DbData = dbData });
        }

        private List<ExcelSample> GetExcelDataList(string fName)
        {
            List<ExcelSample> excelData = new List<ExcelSample>();
            var fileName = $"{Directory.GetCurrentDirectory()}{@"\wwwroot\files"}" + "\\" + fName;

            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

            using (var stream = System.IO.File.Open(fileName, FileMode.Open, FileAccess.Read))
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    // Move to the next row, skipping the header
                    reader.Read();

                    while (reader.Read())
                    {
                        string stockNum = reader.GetValue(0)?.ToString(); // Get stockNum and handle null
                        if (!string.IsNullOrEmpty(stockNum))
                        {
                            excelData.Add(new ExcelSample()
                            {
                                stockNum = stockNum,
                                description = reader.GetValue(1)?.ToString(),
                                price = reader.GetValue(2).ToString(),
                                taxCode = reader.GetValue(3)?.ToString()
                             });
                        }
                    }
                }
            }

            return excelData;
        }

       
    }

    public class OrderViewModel
    {
        public List<ExcelSample> ExcelData { get; set; }
        public List<ExcelSample> DbData { get; set; }

        // Constructor to initialize properties
        public OrderViewModel()
        {
            ExcelData = new List<ExcelSample>();
            DbData = new List<ExcelSample>();
        }
    }
}
