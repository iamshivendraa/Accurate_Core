﻿using Accurate_Core.App_Data;
using Accurate_Core.Models;
using ExcelDataReader;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Text.RegularExpressions;

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
            excelData.AddRange(dbData); // Add the database data to the existing list

            // Pass the list of Excel samples directly to the view
            return View(dbData);
        }

        [HttpPost]
        public IActionResult Index(IFormFile file, [FromServices] Microsoft.AspNetCore.Hosting.IWebHostEnvironment hostingEnvironment)
        {

            try
            {
                if (file == null || file.Length == 0)
                {
                    TempData["ErrorMessage"] = "Please upload a file.";
                    return Json(new { success = false, errorMessage = TempData["ErrorMessage"] });
                }
                string fileName = $"{hostingEnvironment.WebRootPath}\\files\\{file.FileName}";

                using (FileStream fileStream = System.IO.File.Create(fileName))
                {
                    file.CopyTo(fileStream);
                    fileStream.Flush();
                }

                var excelData = GetExcelDataList(file.FileName);

                // Check for duplicate combinations of stock numbers and descriptions between (CUN) and Catalytic Converter
                var duplicateRecords = excelData.GroupBy(x => new { x.stockNum, DescriptionKey = GetTextBetweenCUNAndCatalyticConverter(x.description) })
                                                .Where(g => g.Count() > 1)
                                                .Select(g => new { StockNum = g.Key.stockNum, DescriptionKey = g.Key.DescriptionKey })
                                                .ToList();

                if (duplicateRecords.Any())
                {
                    TempData["ErrorMessage"] = $"Duplicate combinations found. Stock numbers and descriptions between (CUN) and Catalytic Converter must be unique. First duplicate combination: Stock Number: {duplicateRecords.First().StockNum}, Description: {duplicateRecords.First().DescriptionKey}";

                    return Json(new { success = false, errorMessage = TempData["ErrorMessage"] });
                }

                // Check for existing combinations of stock numbers and descriptions between (CUN) and Catalytic Converter in the database
                var existingRecords = _db.ExcelData.Select(x => new { x.stockNum, DescriptionKey = GetTextBetweenCUNAndCatalyticConverter(x.description) }).ToList();

                foreach (var data in excelData)
                {
                    // Check if the combination of stock number and description already exists in the database
                    if (existingRecords.Any(x => x.stockNum == data.stockNum && x.DescriptionKey == GetTextBetweenCUNAndCatalyticConverter(data.description)))
                    {
                        // Display a message about the first duplicate record found
                        TempData["ErrorMessage"] = $"Order with stock number '{data.stockNum}' and description '{data.description}' already exists.";
                        return Json(new { success = false, errorMessage = TempData["ErrorMessage"] });
                    }

                    //Check price format(000.00)
                    if (!IsValidPriceFormat(data.price))
                    {
                        TempData["ErrorMessage"] = $"Invalid price format. Price should be in the numeric format.";
                        return Json(new { success = false, errorMessage = TempData["ErrorMessage"] });
                    }

                    _db.ExcelData.Add(data);
                }

                _db.SaveChanges();

                // Retrieve data from the database
                var dbData = _db.ExcelData.ToList();

                // Pass both Excel data and database data to the view
                TempData.Remove("ErrorMessage");
                TempData.Remove("SuccessMessage");

                // Pass both Excel data and database data to the view
                excelData.AddRange(dbData);
                return Json(new { success = true, data = excelData });

            }
            catch (Exception ex)
            {
                return Json(new { success = false, errorMessage = ex.Message });

            }

        }

        // Function to extract text between (CUN) and Catalytic Converter
        private static string GetTextBetweenCUNAndCatalyticConverter(string input)
        {
            int startIndex = input.IndexOf("(CUN)") + "(CUN)".Length;
            int endIndex = input.IndexOf("Catalytic Converter", startIndex);
            if (startIndex >= 0 && endIndex >= 0)
            {
                return input.Substring(startIndex, endIndex - startIndex).Trim();
            }
            return string.Empty;
        }

        // Function to check if the price has a valid format (000.00)
        private static bool IsValidPriceFormat(string price)
        {
            //string pricePattern = @"^\d{3}(\.\d{2})?$";
            string pricePattern = @"^\d+$";

            return Regex.IsMatch(price, pricePattern);
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
        public IActionResult Edit()
        {
            var orderList = _db.OrderSummaries.ToList();
            return View(orderList);
        }

        [HttpPost]
        public IActionResult Edit(string stockNumber, string grade, string gradePrice)
        {
            try
            {
                // Find the corresponding OrderSummary by StockNumber
                var orderSummary = _db.OrderSummaries.FirstOrDefault(o => o.StockNumber == stockNumber);

                if (orderSummary == null)
                {
                    TempData["ErrorMessage"] = "Order summary not found.";
                    return Json(new { success = false, errorMessage = TempData["ErrorMessage"] });
                }

               
               
                orderSummary.UpdateGradeFields(grade, gradePrice);

                // Save changes to the database
                _db.SaveChanges();
                
                TempData["SuccessMessage"] = "Grade information updated successfully.";
                
                return Json(new { success = true });

            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Error updating grade information: {ex.Message}";
                return Json(new { success = false, errorMessage = TempData["ErrorMessage"] });
            }
        }

    }
}
