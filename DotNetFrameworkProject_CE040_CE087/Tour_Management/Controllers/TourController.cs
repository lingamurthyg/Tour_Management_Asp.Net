using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Collections.Generic;

namespace Tour_Management.Controllers
{
    public class TourController : Controller
    {
        private readonly string _connectionString;
        private readonly IWebHostEnvironment _env;

        public TourController(IConfiguration configuration, IWebHostEnvironment env)
        {
            _connectionString = configuration.GetConnectionString("dbconnection");
            _env = env;
        }

        [HttpGet]
        public IActionResult AddTour()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(AddTourViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("AddTour", model);
            }

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                string query = "INSERT INTO Tours (TourName, Place, Days, Locations, Price, TourInfo, Pic) VALUES (@TOUR_NAME, @PLACE, @DAYS, @LOCATIONS, @PRICE, @TOUR_INFO, @PIC)";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@TOUR_NAME", model.TourName);
                    cmd.Parameters.AddWithValue("@PLACE", model.Place);
                    cmd.Parameters.AddWithValue("@DAYS", model.Days);
                    cmd.Parameters.AddWithValue("@LOCATIONS", model.Locations);
                    cmd.Parameters.AddWithValue("@PRICE", model.Price);
                    cmd.Parameters.AddWithValue("@TOUR_INFO", model.TourInfo);

                    string fileName = "";
                    if (model.TourImage != null)
                    {
                        fileName = Path.GetFileName(model.TourImage.FileName);
                        string filePath = Path.Combine(_env.WebRootPath, "Tour_pics", fileName);
                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await model.TourImage.CopyToAsync(stream);
                        }
                    }

                    cmd.Parameters.AddWithValue("@PIC", fileName);
                    cmd.ExecuteNonQuery();
                }
            }

            TempData["Message"] = "ADD Successful";
            return RedirectToAction("AddTour");
        }
    }

    public class AddTourViewModel
    {
        public string TourName { get; set; }
        public string Place { get; set; }
        public string Days { get; set; }
        public string Locations { get; set; }
        public string Price { get; set; }
        public string TourInfo { get; set; }
        public Microsoft.AspNetCore.Http.IFormFile TourImage { get; set; }
    }
}
