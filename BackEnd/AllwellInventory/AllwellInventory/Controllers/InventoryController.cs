using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;

namespace AllwellInventory.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class InventoryController : ControllerBase
    {
        private SqlConnection con = new SqlConnection("Data Source=localhost;Initial Catalog=AllwellInventory;Integrated Security=True");

        [HttpGet("type", Name = "GetTypes")]
        public List<Models.Type> GetTypes()
        {
            List<Models.Type> typeList = new List<Models.Type>();

            con.Open();

            SqlCommand cmd = new SqlCommand("SELECT * " +
                                            "FROM AllwellInventory.dbo.type", con);

            SqlDataReader rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                Models.Type type = new Models.Type();
                type.TypeId = rd.GetInt32(0);
                type.Name = rd.GetString(1);
                typeList.Add(type);
            }

            con.Close();

            return typeList;
        }

        [HttpGet("location", Name = "GetLocations")]
        public List<Models.Location> GetLocations()
        {
            List<Models.Location> locationList = new List<Models.Location>();

            con.Open();

            SqlCommand cmd = new SqlCommand("SELECT * " +
                                            "FROM AllwellInventory.dbo.location", con);

            SqlDataReader rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                Models.Location location = new Models.Location();
                location.LocationId = rd.GetInt32(0);
                location.Name = rd.GetString(1);
                location.City = rd.GetString(2);
                location.County = rd.GetString(3);
                locationList.Add(location);
            }

            con.Close();

            return locationList;
        }

        [HttpGet("productLite", Name = "GetProductLites")]
        public List<Models.ProductLite> GetProductLites()
        {
            List<Models.ProductLite> productLiteList = new List<Models.ProductLite>();

            con.Open();

            SqlCommand cmd = new SqlCommand("SELECT p.id, p.name, t.name, l.name, p.damaged " +
                                            "FROM AllwellInventory.dbo.products as p inner join " +
                                            "AllwellInventory.dbo.type as t on t.id = p.typeId inner join " +
                                            "AllwellInventory.dbo.location as l on l.id = p.locationId", con);

            SqlDataReader rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                Models.ProductLite productLite = new Models.ProductLite();
                productLite.Id = rd.GetInt32(0);
                productLite.Name = rd.GetString(1);
                productLite.Type = rd.GetString(2);
                productLite.Location = rd.GetString(3);
                productLite.Damaged = rd.GetBoolean(4);
                productLiteList.Add(productLite);
            }

            con.Close();

            return productLiteList;
        }

        [HttpGet("product/{productId}", Name = "GetProductDetail")]
        public Models.ProductDetail GetProductDetail([FromRoute(Name = "productId")] int productId)
        {
            Models.ProductDetail productDetail = new Models.ProductDetail();

            con.Open();

<<<<<<< HEAD
            SqlCommand cmd = new SqlCommand("SELECT p.name, t.name, p.cost, l.name, p.condition, p.damaged, p.serialNo, p.id " +
                                            "FROM AllwellInventory.dbo.products as p inner join " +
                                            "AllwellInventory.dbo.type as t on t.id = p.typeId inner join " +
                                            "AllwellInventory.dbo.location as l on l.id = p.locationId " +
=======
            SqlCommand cmd = new SqlCommand("SELECT p.id, p.name, p.typeId, p.cost, p.locationId, p.condition, p.damaged, p.serialNo " +
                                            "FROM AllwellInventory.dbo.products as p " +
>>>>>>> Ian_branch
                                            "WHERE p.id = " + productId, con);

            SqlDataReader rd = cmd.ExecuteReader();

            while (rd.Read())
            {
<<<<<<< HEAD
                productDetail.Name = rd.GetString(0);
                productDetail.Type = rd.GetString(1);
                productDetail.Cost = rd.GetDecimal(2);
                productDetail.Location = rd.GetString(3);
                productDetail.Condition = rd.GetString(4);
                productDetail.Damaged = rd.GetBoolean(5);
                productDetail.SerialNo = rd.GetString(6);
                productDetail.ID = rd.GetInt32(7);
=======
                productDetail.ProductId = rd.GetInt32(0);
                productDetail.Name = rd.GetString(1);
                productDetail.TypeId = rd.GetInt32(2);
                productDetail.Cost = rd.GetDecimal(3);
                productDetail.LocationId = rd.GetInt32(4);
                productDetail.Condition = rd.GetString(5);
                productDetail.Damaged = rd.GetBoolean(6);
                productDetail.SerialNo = rd.GetString(7);
>>>>>>> Ian_branch
            }

            con.Close();

            return productDetail;
        }

        [HttpPut("product/{id}/{name}/{typeId}/{cost}/{locationId}/{condition}/{damaged}/{serialNo}", Name = "PutProductDetail")]
        public Models.ProductDetail PutProductDetail([FromRoute(Name = "id")] int id,
                                                        [FromRoute(Name = "name")] string name,
                                                        [FromRoute(Name = "typeId")] int typeId,
                                                        [FromRoute(Name = "cost")] double cost,
                                                        [FromRoute(Name = "locationId")] int locationId,
                                                        [FromRoute(Name = "condition")] string condition,
                                                        [FromRoute(Name = "damaged")] bool damaged,
                                                        [FromRoute(Name = "serialNo")] string serialNo)
        {
            con.Open();

            SqlCommand cmd = new SqlCommand("update AllwellInventory.dbo.products set name = '" + name + "'," +
                "typeId = " + typeId + ", cost = " + cost + ", locationId = " + locationId + ", condition = '" +
                condition + "', damaged = " + (damaged ? 1 : 0) + ", serialNo = '" + serialNo +
                "' where id =" + id, con);

            cmd.ExecuteNonQuery();

            con.Close();

            return GetProductDetail(id);
        }

        [HttpPost("product/{name}/{typeId}/{cost}/{locationId}/{condition}/{damaged}/{serialNo}", Name = "AddProduct")]
        public bool AddProduct([FromRoute(Name = "name")] string name,
                                                [FromRoute(Name = "typeId")] int typeId,
                                                [FromRoute(Name = "cost")] double cost,
                                                [FromRoute(Name = "locationId")] int locationId,
                                                [FromRoute(Name = "condition")] string condition,
                                                [FromRoute(Name = "damaged")] bool damaged,
                                                [FromRoute(Name = "serialNo")] string serialNo)
        {
            con.Open();

            SqlCommand cmd = new SqlCommand("insert into AllwellInventory.dbo.products values('" + name +  "', " + typeId + ", " +
                cost + ", null, " + locationId + ", '" + condition + "', " + (damaged ? 1 : 0) + ", '" + serialNo + "')", con);

            cmd.ExecuteNonQuery();

            con.Close();

            return true;
        }
    }
}