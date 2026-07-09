using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using System.IO;
using Tour_Management.Models;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Tour_Management.Controllers
{
    public class TourController : Controller
    {
        private readonly string _connectionString;
        private readonly IWebHostEnvironment _env;

        public TourController(IConfiguration configuration, IWebHostEnvironment env)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
            _env = env;
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
                string uploadsFolder = Path.Combine(_env.WebRootPath, "Tour_pics");
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

        [HttpGet]
        public IActionResult DisplayTours()
        {
            List<TourDisplayViewModel> tours = new List<TourDisplayViewModel>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                string sql = "SELECT [TOUR_NAME], [pic], [PRICE], [DAYS], [LOCATIONS], [TOUR_ID] FROM [Tour]";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            tours.Add(new TourDisplayViewModel
                            {
                                TourName = reader["TOUR_NAME"].ToString(),
        [HttpGet]
        public IActionResult ManageTours()
        {
            List<TourCrudViewModel> tours = new List<TourCrudViewModel>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                string sql = "SELECT * FROM [Tour]";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            tours.Add(new TourCrudViewModel
                            {
                                TourId = Convert.ToInt32(reader["TOUR_ID"]),
                                TourName = reader["TOUR_NAME"].ToString(),
                                Place = reader["PLACE"].ToString(),
                                Days = reader["DAYS"].ToString(),
                                Price = reader["PRICE"].ToString(),
                                Locations = reader["LOCATIONS"].ToString(),
                                TourInfo = reader["TOUR_INFO"].ToString(),
                                Pic = reader["pic"].ToString()
                            });
                        }
                    }
                }
            }
            return View(tours);
        }

        [HttpPost]
        public IActionResult DeleteTour(int id)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                string sql = "DELETE FROM [Tour] WHERE [TOUR_ID]=@id";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                }
            }
            return RedirectToAction("ManageTours");
        }

                        }
                    }
                }
            }
            return View(tours);
        }
            }

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                string sql = "INSERT INTO Tours (TourName, Days, Price, Locations, TourInfo, Pic) VALUES (@TourName, @Days, @Price, @Locations, @TourInfo, @Pic)";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@TourName", model.TourName ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Days", model.Days ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Price", model.Price ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Locations", model.Locations ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@TourInfo", model.TourInfo ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Pic", fileName ?? (object)DBNull.Value);
                    cmd.ExecuteNonQuery();
                }
            }

            return RedirectToAction("Index", "Home");
        }
    }
}
