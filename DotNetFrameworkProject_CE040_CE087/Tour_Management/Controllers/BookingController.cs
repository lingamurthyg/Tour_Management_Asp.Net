using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Tour_Management.Models;

namespace Tour_Management.Controllers
{
    public class BookingController : Controller
    {
        private readonly string _connectionString;

        public BookingController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        [HttpGet]
        public IActionResult Order(int id)
        {
            // In a real app, we'd fetch tour name by id
            return View();
        }

        [HttpPost]
        public IActionResult Book(BookingViewModel model)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                string insertQuery = "insert into booking(TOUR_NAME,PLACE,Email,FirstName) values(@TOUR_NAME,@PLACE,@Email,@FirstName)";
                using (SqlCommand com = new SqlCommand(insertQuery, conn))
                {
                    com.Parameters.AddWithValue("@TOUR_NAME", model.TourName ?? (object)DBNull.Value);
                    com.Parameters.AddWithValue("@PLACE", model.City ?? (object)DBNull.Value);
                    com.Parameters.AddWithValue("@Email", model.MobileNumber ?? (object)DBNull.Value);
                    com.Parameters.AddWithValue("@FirstName", model.CustomerName ?? (object)DBNull.Value);
                    com.ExecuteNonQuery();
                }
            }
            return RedirectToAction("MyBookings");
        }

        public IActionResult MyBookings()
        {
        [HttpGet]
        public IActionResult MyBookings()
        {
            List<MyBookingViewModel> bookings = new List<MyBookingViewModel>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                string sql = "SELECT [TOUR_NAME], [TOUR_ID] FROM [booking]";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            bookings.Add(new MyBookingViewModel
                            {
                                TourName = reader["TOUR_NAME"].ToString(),
                                TourId = Convert.ToInt32(reader["TOUR_ID"])
            List<AllBookingViewModel> bookings = new List<AllBookingViewModel>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                string sql = "SELECT * FROM [booking]";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            bookings.Add(new AllBookingViewModel
                            {
                                TourId = Convert.ToInt32(reader["TOUR_ID"]),
                                TourName = reader["TOUR_NAME"].ToString(),
                                Place = reader["PLACE"].ToString(),
                                Email = reader["Email"].ToString(),
                                FirstName = reader["FirstName"].ToString()
                            });
                        }
                    }
                }
            }
            return View(bookings);
            return View(bookings);
        }

        [HttpPost]
        public IActionResult DeleteBooking(int tourId)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                string sql = "DELETE FROM [booking] WHERE [TOUR_ID]=@TOUR_ID";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@TOUR_ID", tourId);
                    cmd.ExecuteNonQuery();
                }
            }
            return RedirectToAction("MyBookings");
        }

            return View();
        }
    }
}
