using AllwellInventory.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;


namespace AllwellInventory.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private SqlConnection con = new SqlConnection("Data Source=localhost;Initial Catalog=AllwellInventory;Integrated Security=True");

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

        /**
         * GET: api/<EmployeeController>
         * To get all employees from the database.
         * return a list of all employees
         **/
        [HttpGet]
        public JsonResult Get()
        {
            List<Employee> employees = new List<Employee>();
            
            string query = "Select * from dbo.employee order by fName asc";

            using (SqlConnection conn = new SqlConnection(sqlDataSource))
            {
                using (SqlCommand command = new SqlCommand(query, conn))
                {
                    conn.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Employee e = new Employee();
                            e.Id = reader.GetInt32(0);
                            e.fName = reader.GetString(1).Trim();
                            e.lName = reader.GetString(2).Trim();
                            if (!reader.IsDBNull(3))
                                e.Username = reader.GetString(3).Trim();
                            if (!reader.IsDBNull(4))
                                e.Password = reader.GetString(4).Trim();
                            e.IsAdmin = reader.GetBoolean(5);

                            employees.Add(e);
                        }
                    }
                }
            }
            return new JsonResult(employees);
        }

        /**
        * PUT: api/<EmployeeController>
        * To edit an employee in the database.
        * return employee detail
        **/
        [HttpPut("{id}/{fName}/{lName}/{username}/{password}/{isAdmin}/", Name = "PutEmployeeDetail")]
        public Models.Employee PutEmployeeDetail([FromRoute(Name = "id")] int id,
                                                        [FromRoute(Name = "fName")] string fName,
                                                        [FromRoute(Name = "lName")] string lName,
                                                        [FromRoute(Name = "username")] string username,
                                                        [FromRoute(Name = "password")] string password,
                                                        [FromRoute(Name = "isAdmin")] bool isAdmin)
        {
            con.Open();

            SqlCommand cmd = new SqlCommand("update AllwellInventory.dbo.employee set fName = '" + fName + "'," +
                "lName = '" + lName + "', username = '" + username + "', password = '" + password + "', isAdmin = " + 
                (isAdmin ? 1 : 0) + " where id =" + id, con);

            cmd.ExecuteNonQuery();

            con.Close();

            return Get(id);
        }

        /**
        * GET: api/<EmployeeController>
        * To get location.
        * return location list
        **/

        /**
         * POST: api/<EmployeeController>
         * To add an employee to the database.
        * return employee detail
           **/
        [HttpPost("{fName}/{lName}/{username}/{password}/{isAdmin}", Name = "AddEmployee")]
        public bool AddEmployee([FromRoute(Name = "fName")] string fName,
                                        [FromRoute(Name = "lName")] string lName,
                                        [FromRoute(Name = "username")] string username,
                                        [FromRoute(Name = "password")] string password,
                                        [FromRoute(Name = "isAdmin")] bool isAdmin)
        {
            con.Open();

            SqlCommand cmd = new SqlCommand("insert into AllwellInventory.dbo.employee values('" + fName + "', '" + lName + "', '" +
                username + "', '" + password + "', " + (isAdmin ? 1 : 0) + ", null)", con);

            cmd.ExecuteNonQuery();

            con.Close();

            return true;
        }

        /**
         * GET api/<EmployeeController>/5
         * Get employee by Id.
         **/
        [HttpGet("{id}")]
        public Employee Get(int id)
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
                            if (!reader.IsDBNull(3))
                                e.Username = reader.GetString(3).Trim();
                            if (!reader.IsDBNull(4))
                                e.Password = reader.GetString(4).Trim();
                            e.IsAdmin = reader.GetBoolean(5);

                        }
                    }
                }

            }
            if (e.Id == 0)
            {
                return null;
            }

            return e;
        }

        
        /**
         * GET api/<EmployeeController>/string/string
         * Get an employee using the username and password.
         * Return a Jason Object containing employee information if
         * there is a match, a Json object containing fals otherwise.
         **/
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
