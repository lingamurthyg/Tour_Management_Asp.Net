using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Tour_Management.Controllers
{
    public partial class TourController : Controller
    {
        private readonly string _connectionString;

        public TourController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        [HttpGet]
        public IActionResult DisplayTours()
        {
            List<TourDisplayViewModel> tours = new List<TourDisplayViewModel>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "SELECT [TOUR_NAME], [pic], [PRICE], [DAYS], [LOCATIONS], [TOUR_ID] FROM [Tour]";
                SqlCommand com = new SqlCommand(query, conn);
                conn.Open();
                using (SqlDataReader reader = com.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        tours.Add(new TourDisplayViewModel
                        {
                            TourName = reader["TOUR_NAME"].ToString(),
                            Days = reader["DAYS"].ToString(),
                            Locations = reader["LOCATIONS"].ToString(),
                            TourId = (int)reader["TOUR_ID"],
                            Pic = reader["pic"].ToString()
                        });
                    }
                }
            }

            return View(tours);
        }
    }
}
