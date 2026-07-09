using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Tour_Management.Models;

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
        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(UserSignUpViewModel model)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                string insertQuery = "insert into UserInfo(Email,FirstName,LastName,Gender,Password,dob,Street,City,State) values(@email,@FirstName,@LastName,@Gender,@Password,@dob,@Street,@City,@State)";
                using (SqlCommand com = new SqlCommand(insertQuery, conn))
                {
                    com.Parameters.AddWithValue("@Email", model.Email ?? (object)DBNull.Value);
                    com.Parameters.AddWithValue("@FirstName", model.FirstName ?? (object)DBNull.Value);
                    com.Parameters.AddWithValue("@LastName", model.LastName ?? (object)DBNull.Value);
                    com.Parameters.AddWithValue("@Gender", model.Gender ?? (object)DBNull.Value);
                    com.Parameters.AddWithValue("@Password", model.Password ?? (object)DBNull.Value);
                    com.Parameters.AddWithValue("@dob", model.Dob ?? (object)DBNull.Value);
                    com.Parameters.AddWithValue("@Street", model.Street ?? (object)DBNull.Value);
                    com.Parameters.AddWithValue("@City", model.City ?? (object)DBNull.Value);
                    com.Parameters.AddWithValue("@State", model.State ?? (object)DBNull.Value);
                    com.ExecuteNonQuery();
                }
            }
            return RedirectToAction("Login");
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
            List<UserCrudViewModel> users = new List<UserCrudViewModel>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                string sql = "SELECT Email, FirstName, LastName, Gender, Password, City FROM UserInfo";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            users.Add(new UserCrudViewModel
                            {
                                Email = reader["Email"].ToString(),
                                FirstName = reader["FirstName"].ToString(),
                                LastName = reader["LastName"].ToString(),
                                Gender = reader["Gender"].ToString(),
                                Password = reader["Password"].ToString(),
                                City = reader["City"].ToString()
                            });
                        }
                    }
                }
            }
            return View(users);
                    string password = passComm.ExecuteScalar()?.ToString() ?? "";
                    if (password == model.Password)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
            }
            return View(model);
        }
        }
    }
}
