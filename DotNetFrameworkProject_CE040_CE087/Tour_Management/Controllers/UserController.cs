using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Tour_Management.Models;

namespace Tour_Management.Controllers
{
    public partial class UserController : Controller
    {
        private readonly string _connectionString;

        public UserController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("dbconnection");
        }

        [HttpGet]
        public async Task<IActionResult> UserCrud()
        {
            var users = new List<UserViewModel>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                string query = "SELECT Email, FirstName, LastName, Gender, Password, City FROM UserInfo";
                using (SqlCommand com = new SqlCommand(query, conn))
                {
                    using (SqlDataReader reader = await com.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            users.Add(new UserViewModel
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
        }

        [HttpGet]
        public IActionResult Edit(string id)
        {
            // Implementation for edit
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UserViewModel model)
        {
            // Implementation for update
            return RedirectToAction("UserCrud");
        }
    }
}
