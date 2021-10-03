using AllwellInventory.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AllwellInventory.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private string sqlDataSource;
        /**
         * Constructor for EmployeesController
         **/
        public EmployeeController(IConfiguration configuration)
        {
            _configuration = configuration;
            sqlDataSource = _configuration.GetConnectionString("Database");
        }

        // GET: api/<EmployeeController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<EmployeeController>/5
        [HttpGet("{id}")]
        public JsonResult Get(int id)
        {
            Employee e = new Employee();
            string query = "Select * from dbo.employee where Id = @Id";

            using (SqlConnection conn = new SqlConnection(sqlDataSource))
            {
                using (SqlCommand command = new SqlCommand(query, conn))
                {
                    conn.Open();
                    command.Parameters.AddWithValue("@Id", id);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            e.Id = reader.GetInt32(0);
                            e.fName = reader.GetString(1).Trim();
                            e.lName = reader.GetString(2).Trim();
                            e.Username = reader.GetString(3).Trim();
                            e.Password = reader.GetString(4).Trim();
                            e.IsAdmin = reader.GetBoolean(5);
                            
                        }
                    }
                }

            }
            if (e.Id == 0)
            {
                return new JsonResult(false);
            }

            return new JsonResult(e);
        }

        // GET api/<EmployeeController>/5
        [HttpGet("{username}/{password}")]
        public JsonResult Get(string username, string password)
        {

            Employee e = new Employee();
            string query = @"Select * from dbo.employee where username = @username and 
                                password = @password";

            using (SqlConnection conn = new SqlConnection(sqlDataSource))
            {
                using (SqlCommand command = new SqlCommand(query, conn))
                {
                    conn.Open();
                    command.Parameters.AddWithValue("@username", username);
                    command.Parameters.AddWithValue("@password", password);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            e.Id = reader.GetInt32(0);
                            e.fName = reader.GetString(1).Trim();
                            e.lName = reader.GetString(2).Trim();
                            e.Username = reader.GetString(3).Trim();
                            e.Password = reader.GetString(4).Trim();
                            e.IsAdmin = reader.GetBoolean(5);
                            
                        }
                    }
                }
                if (e.Id == 0)
                {
                    return new JsonResult(false);
                }

                return new JsonResult(e);
            }
        }

    }
}
