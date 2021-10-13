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
                type.Name = rd.GetString(1);
                typeList.Add(type);
            }
            return typeList;
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
            return productLiteList;
        }

        [HttpGet("product/{productId}", Name = "GetProductDetail")]
        public Models.ProductDetail GetProductDetail([FromRoute(Name = "productId")] int productId)
        {
            Models.ProductDetail productDetail = new Models.ProductDetail();

            con.Open();

            SqlCommand cmd = new SqlCommand("SELECT p.id, p.name, t.name, p.cost, l.name, p.condition, p.damaged, p.serialNo " +
                                            "FROM AllwellInventory.dbo.products as p inner join " +
                                            "AllwellInventory.dbo.type as t on t.id = p.typeId inner join " +
                                            "AllwellInventory.dbo.location as l on l.id = p.locationId " +
                                            "WHERE p.id = " + productId, con);

            SqlDataReader rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                productDetail.productId = rd.GetInt32(0);
                productDetail.Name = rd.GetString(1);
                productDetail.Type = rd.GetString(2);
                productDetail.Cost = rd.GetDecimal(3);
                productDetail.Location = rd.GetString(4);
                productDetail.Condition = rd.GetString(5);
                productDetail.Damaged = rd.GetBoolean(6);
                productDetail.SerialNo = rd.GetString(7);
            }
            return productDetail;
        }
        [AllowAnonymous]
        [IgnoreAntiforgeryToken]
        [HttpPut("product/", Name = "PutProductDetail")]
        public Models.ProductDetail PutProductDetail([FromBody] Models.ProductDetail productDetail)
        {
            con.Open();

            SqlCommand cmd = new SqlCommand("update AllwellInventory.dbo.products set name = " + productDetail.Name + "," +
                "typeId = " + productDetail.Type + ", cost = " + productDetail.Cost + ", locationId = " + productDetail.Location + ", condition = " +  
                productDetail.Condition + ", damaged = " + productDetail.Damaged + ", serialNo = " + productDetail.SerialNo +
                "where id =" + productDetail.productId, con);

            cmd.ExecuteNonQuery();

            return GetProductDetail(productDetail.productId);
        }
    }
}