using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Tour_Management.Models;

namespace Tour_Management.Controllers
{
    public partial class BookingController : Controller
    {
        private readonly string _connectionString;

        public BookingController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("dbconnection");
        }

        [HttpGet]
        public async Task<IActionResult> MyBooking()
        {
            var bookings = new List<MyBookingViewModel>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                string query = "SELECT [TOUR_NAME], [TOUR_ID] FROM [booking]";
                using (SqlCommand com = new SqlCommand(query, conn))
                {
                    using (SqlDataReader reader = await com.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            bookings.Add(new MyBookingViewModel
                            {
                                TourName = reader["TOUR_NAME"].ToString(),
                                TourId = Convert.ToInt32(reader["TOUR_ID"])
                            });
                        }
                    }
                }
            }
            return View(bookings);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteBooking(int id)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                string deleteQuery = "Delete from [booking] Where [TOUR_ID]=@TOUR_ID";
                using (SqlCommand com = new SqlCommand(deleteQuery, conn))
                {
                    com.Parameters.AddWithValue("@TOUR_ID", id);
                    await com.ExecuteNonQueryAsync();
                }
            }
            return RedirectToAction("MyBooking");
        }
    }
}
