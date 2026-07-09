using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Threading.Tasks;
using System.Collections.Generic;
using Tour_Management.Models;

namespace Tour_Management.Controllers
{
    public class TourController : Controller
    {
        private readonly string _connectionString;
        private readonly IWebHostEnvironment _environment;

        public TourController(IConfiguration configuration, IWebHostEnvironment environment)
        {
            _connectionString = configuration.GetConnectionString("dbconnection");
            _environment = environment;
        }

        [HttpGet]
        public IActionResult AddTour()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(TourViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("AddTour", model);
            }

            string fileName = "";
            if (model.TourImage != null)
            {
                fileName = Path.GetFileName(model.TourImage.FileName);
                string uploadsFolder = Path.Combine(_environment.WebRootPath, "Tour_pics");
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }
                string filePath = Path.Combine(uploadsFolder, fileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await model.TourImage.CopyToAsync(stream);
                }
            }

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                string insertQuery = "insert into Tour(TOUR_NAME,PLACE,DAYS,PRICE,LOCATIONS,TOUR_INFO,pic) values(@TOUR_NAME,@PLACE,@DAYS,@PRICE,@LOCATIONS,@TOUR_INFO,@pic)";
                using (SqlCommand com = new SqlCommand(insertQuery, conn))
                {
                    com.Parameters.AddWithValue("@TOUR_NAME", model.TourName);
                    com.Parameters.AddWithValue("@PLACE", model.Place);
                    com.Parameters.AddWithValue("@DAYS", model.Days);
                    com.Parameters.AddWithValue("@PRICE", model.Price);
                    com.Parameters.AddWithValue("@LOCATIONS", model.Locations);
                    com.Parameters.AddWithValue("@TOUR_INFO", model.TourInfo);
                    com.Parameters.AddWithValue("@pic", fileName);

                    await com.ExecuteNonQueryAsync();
                }
            }

            return Content("ADD Successful");
        }

        [HttpGet]
        public async Task<IActionResult> DisplayTours()
        {
            var tours = new List<TourDisplayViewModel>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                string query = "SELECT [TOUR_NAME], [pic], [PRICE], [DAYS], [LOCATIONS], [TOUR_ID] FROM [Tour]";
                using (SqlCommand com = new SqlCommand(query, conn))
                {
                    using (SqlDataReader reader = await com.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            tours.Add(new TourDisplayViewModel
                            {
                                TourName = reader["TOUR_NAME"].ToString(),
                                Pic = reader["pic"].ToString(),
                                Price = reader["PRICE"].ToString(),
                                Days = reader["DAYS"].ToString(),
                                Locations = reader["LOCATIONS"].ToString(),
                                TourId = Convert.ToInt32(reader["TOUR_ID"])
                            });
                        }
                    }
                }
            }
            return View(tours);
        }
    }
}
