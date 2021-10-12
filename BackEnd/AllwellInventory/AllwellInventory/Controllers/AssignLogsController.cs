using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using AllwellInventory.Models;
using Microsoft.Data.SqlClient;
using System.Net;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AllwellInventory.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssignLogsController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private string sqlDataSource;

        public AssignLogsController(IConfiguration configuration)
        {
            _configuration = configuration;
            sqlDataSource = _configuration.GetConnectionString("Database");
        }


        // GET: api/<AssignLogsController>
        [HttpGet("{productId}")]
        public JsonResult GetLogs(int productId)
        {
            List<AssignLog> logsList = new List<AssignLog>();
            string query = "select * from assign_log " +
                            "where productId = @productId " +
                            "order by returnedDate desc";

            using (SqlConnection conn = new SqlConnection(sqlDataSource))
            {
                using (SqlCommand command = new SqlCommand(query, conn))
                {
                    conn.Open();
                    command.Parameters.AddWithValue("@productId", productId);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            AssignLog log = new AssignLog();

                            log.Id = reader.GetInt32(0);
                            log.EmployeeId = reader.GetInt32(1);
                            log.ProductId = reader.GetInt32(2);
                            log.AssignedDate = reader.GetDateTime(3);

                            try
                            {
                                log.ReturnedDate = reader.GetDateTime(4);
                            }
                            catch(Exception e)
                            {
                                log.ReturnedDate = null;
                            }

                            logsList.Add(log);

                        }
                    }
                }

            }
            if (logsList.Count() == 0)
            {
                return new JsonResult(false);
            }

            return new JsonResult(logsList);
        }

        [HttpPost("returnProduct/{productId}", Name = "returnProduct")]
        public JsonResult returnProduct(int productId)
        {
            string query = "UPDATE assign_log "
                           + "SET returnedDate = @date "
                           + "WHERE productId = @productId " +
                           "AND returnedDate IS NULL";

            using (SqlConnection conn = new SqlConnection(sqlDataSource))
            {
                using (SqlCommand command = new SqlCommand(query, conn))
                {
                    conn.Open();
                    command.Parameters.AddWithValue("@productId", productId);
                    command.Parameters.AddWithValue("@date", DateTime.Now);
                    command.ExecuteNonQuery();
                    
                }

            }
            return new JsonResult("Success");
        }

    }
}
