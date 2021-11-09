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
    public class LocationController : ControllerBase
    {
        private SqlConnection con = new SqlConnection("Data Source=localhost;Initial Catalog=AllwellInventory;Integrated Security=True");

        private readonly IConfiguration _configuration;
        private string sqlDataSource;

        /**
         * Constructor for LocationsController
         **/
        public LocationController(IConfiguration configuration)
        {
            _configuration = configuration;
            sqlDataSource = _configuration.GetConnectionString("Database");
        }

        /**
         * GET: <LocationController>
         * To get all locations from the database.
         * return a list of all locations
         **/
        [HttpGet]
        public JsonResult GetLocations()
        {
            List<Location> locations = new List<Location>();
            
            string query = "Select * from AllwellInventory.dbo.location order by name asc";

            using (SqlConnection conn = new SqlConnection(sqlDataSource))
            {
                using (SqlCommand command = new SqlCommand(query, conn))
                {
                    conn.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Location e = new Location();
                            e.LocationId = reader.GetInt32(0);
                            e.Name = reader.GetString(1).Trim();
                            if (!reader.IsDBNull(2))
                                e.City = reader.GetString(2).Trim();
                            if (!reader.IsDBNull(3))
                                e.County = reader.GetString(3).Trim();
                            locations.Add(e);
                        }
                    }
                }
            }
            return new JsonResult(locations);
        }

        /**
        * PUT: <LocationController>
        * To edit an location in the database.
        * return location detail
        **/
        [HttpPut("{LocationId}/{Name}/{City}/{County}", Name = "PutLocation")]
        public Models.Location PutLocationDetail([FromRoute(Name = "LocationId")] int LocationId,
                                                        [FromRoute(Name = "Name")] string Name,
                                                        [FromRoute(Name = "City")] string City,
                                                        [FromRoute(Name = "County")] string County)
        {
            con.Open();

            SqlCommand cmd = new SqlCommand("update AllwellInventory.dbo.location set Name = '" + Name + "'," +
                "City = '" + City + "', County = '" + County + "' where id =" + LocationId, con);

            cmd.ExecuteNonQuery();

            con.Close();

            return Get(LocationId);
        }

        /**
        * GET: api/<LocationController>
        * To get location.
        * return location list
        **/

        /**
         * POST: <LocationController>
         * To add an location to the database.
        * return location detail
           **/
        [HttpPost("{Name}/{City}/{County}", Name = "AddLocation")]
        public bool AddLocation([FromRoute(Name = "Name")] string Name,
                                        [FromRoute(Name = "City")] string City,
                                        [FromRoute(Name = "County")] string County)
        {
            con.Open();

            SqlCommand cmd = new SqlCommand("insert into AllwellInventory.dbo.location values('" + Name + "', '" + City + "', '" +
                County + "')", con);

            cmd.ExecuteNonQuery();

            con.Close();

            return true;
        }

        /**
         * GET <LocationController>
         * Get location by id.
         **/
        [HttpGet("{id}")]
        public Location Get(int id)
        {
            Location e = new Location();
            string query = "Select * from dbo.location where id = @Id";

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
                            e.LocationId = reader.GetInt32(0);
                            e.Name = reader.GetString(1).Trim();
                            if (!reader.IsDBNull(2))
                                e.City = reader.GetString(2).Trim();
                            if (!reader.IsDBNull(3))
                                e.County = reader.GetString(3).Trim();
                        }
                    }
                }

            }
            if (e.LocationId == 0)
            {
                return null;
            }

            return e;
        
        }

    }
}
