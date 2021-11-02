using AllwellInventory.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;


namespace AllwellInventory.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TypeController : ControllerBase
    {
        private SqlConnection con = new SqlConnection("Data Source=localhost;Initial Catalog=AllwellInventory;Integrated Security=True");

        private readonly IConfiguration _configuration;
        private string sqlDataSource;

        /**
         * Constructor for TypesController
         **/
        public TypeController(IConfiguration configuration)
        {
            _configuration = configuration;
            sqlDataSource = _configuration.GetConnectionString("Database");
        }

        /**
         * GET: <TypeController>
         * To get all types from the database.
         * return a list of all types
         **/
        [HttpGet]
        public JsonResult GetTypes()
        {
            List<Type> types = new List<Type>();
            
            string query = "Select * from AllwellInventory.dbo.type order by name asc";

            using (SqlConnection conn = new SqlConnection(sqlDataSource))
            {
                using (SqlCommand command = new SqlCommand(query, conn))
                {
                    conn.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Type e = new Type();
                            e.TypeId = reader.GetInt32(0);
                            e.Name = reader.GetString(1).Trim();
                            types.Add(e);
                        }
                    }
                }
            }
            return new JsonResult(types);
        }

        /**
        * PUT: <TypeController>
        * To edit an type in the database.
        * return type detail
        **/
        [HttpPut("{TypeId}/{Name}", Name = "PutType")]
        public Models.Type PutTypeDetail([FromRoute(Name = "TypeId")] int TypeId,
                                                        [FromRoute(Name = "Name")] string Name)
        {
            con.Open();

            SqlCommand cmd = new SqlCommand("update AllwellInventory.dbo.type set Name = '" + Name + 
                "' where id =" + TypeId, con);

            cmd.ExecuteNonQuery();

            con.Close();

            return Get(TypeId);
        }

        /**
         * POST: <TypeController>
         * To add an type to the database.
        * return type detail
           **/
        [HttpPost("{Name}", Name = "AddType")]
        public bool AddType([FromRoute(Name = "Name")] string Name)
        {
            con.Open();

            SqlCommand cmd = new SqlCommand("insert into AllwellInventory.dbo.type values('" + Name + "')", con);

            cmd.ExecuteNonQuery();

            con.Close();

            return true;
        }

        /**
        * DELETE: <TypeController>
        * To remove a type from the database.
        * return boolean
        **/
        //[HttpDelete("{TypeId}", Name = "DeleteType")]
        //public bool DeleteType([FromRoute(Name = "TypeId")] int TypeId)
        //{
        //    con.Open();

        //    SqlCommand cmd = new SqlCommand("delete from AllwellInventory.dbo.type where id = " + TypeId, con);

        //    cmd.ExecuteNonQuery();

        //    con.Close();

        //    return true;
        //}

        /**
         * GET <TypeController>
         * Get type by id.
         **/
        [HttpGet("{id}")]
        public Type Get(int id)
        {
            Type e = new Type();
            string query = "Select * from dbo.type where id = @Id";

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
                            e.TypeId = reader.GetInt32(0);
                            e.Name = reader.GetString(1).Trim();
                        }
                    }
                }

            }
            if (e.TypeId == 0)
            {
                return null;
            }

            return e;
        
        }

    }
}
