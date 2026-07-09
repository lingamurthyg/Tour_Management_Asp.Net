using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Tour_Management.Controllers
{
    public class UserController : Controller
    {
        private readonly string _connectionString;

        public UserController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        [HttpGet]
        public IActionResult SignUpForm()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(SignUpViewModel model)
        {
            if (model.Password != model.ConfirmPassword)
            {
                ModelState.AddModelError("", "Passwords do not match.");
                return View("SignUpForm", model);
            }

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "INSERT INTO Users (Email, FirstName, LastName, Gender, Password, Dob, Street, City, State) VALUES (@Email, @FirstName, @LastName, @Gender, @Password, @Dob, @Street, @City, @State)";
                SqlCommand com = new SqlCommand(query, conn);
                com.Parameters.AddWithValue("@Email", model.Email);
                com.Parameters.AddWithValue("@FirstName", model.FirstName);
                com.Parameters.AddWithValue("@LastName", model.LastName);
                com.Parameters.AddWithValue("@Gender", model.Gender);
                com.Parameters.AddWithValue("@Password", model.Password);
                com.Parameters.AddWithValue("@Dob", model.Dob);
                com.Parameters.AddWithValue("@Street", model.Street);
                com.Parameters.AddWithValue("@City", model.City);
                com.Parameters.AddWithValue("@State", model.State);

                conn.Open();
                com.ExecuteNonQuery();
            }

            return RedirectToAction("MainProfilePage", "Home");
        }

        [HttpGet]
        public IActionResult UserCrud()
        {
            return View();
        }
    }
}
