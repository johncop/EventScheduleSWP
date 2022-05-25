using EventProjectSWP.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace EventProjectSWP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public UserController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet("get-list")]
        public JsonResult Get()
        {
            string query = @"select users_id, users_name, users_phone, users_address, users_email from dbo.tblUser";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("EventAppConn");
            SqlDataReader myReader;
            using(SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using(SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();

                }
            }
            return new JsonResult(table);
        }


        /* [HttpGet("get-user-by-id")]
         public JsonResult GetUserByID(string id)
         {
             string query = @"select users_id, users_name, users_phone, users_address, users_email from dbo.tblUser
              where users_id = @users_id";


             DataTable table = new DataTable();
             string sqlDataSource = _configuration.GetConnectionString("EventAppConn");
             SqlDataReader myReader;
             using (SqlConnection myCon = new SqlConnection(sqlDataSource))
             {
                 myCon.Open();
                 using (SqlCommand myCommand = new SqlCommand(query, myCon))
                 {
                     myCommand.Parameters.AddWithValue("@users_id", id);
                     myReader = myCommand.ExecuteReader();
                     table.Load(myReader);
                     myReader.Close();
                     myCon.Close();

                 }
             }
             return new JsonResult(table);
         }
 */


        [HttpGet("get-user-by-name")]
        public JsonResult GetUserByID(string name)
        {
            string query = @"select users_id, users_name, users_phone, users_address, users_email from dbo.tblUser
             where users_name = @users_name";


            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("EventAppConn");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@users_name", name);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();

                }
            }
            return new JsonResult(table);
        }




        [HttpPost("add")]
        public JsonResult Post(User u)
        {
            string query = @"insert into dbo.tblUser values (@users_id, @users_name, @users_phone, @users_address, @users_email)";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("EventAppConn");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@users_id", u.UserId);
                    myCommand.Parameters.AddWithValue("@users_name", u.UserName);
                    myCommand.Parameters.AddWithValue("@users_phone", u.Phone);
                    myCommand.Parameters.AddWithValue("@users_address", u.Address);
                    myCommand.Parameters.AddWithValue("@users_email", u.Email);
                    myReader =myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();

                }
            }
            return new JsonResult("add successfully");
        }


        [HttpPut("update")]
        public JsonResult Put(User u)
        {
            string query = @"update dbo.tblUser 
                             set users_name = @users_name,
                                 users_phone = @users_phone,
                                 users_address = @users_address,
                                 users_email = @users_email
                             where users_id = @users_id"
                             ;

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("EventAppConn");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@users_id", u.UserId);
                    myCommand.Parameters.AddWithValue("@users_name", u.UserName);
                    myCommand.Parameters.AddWithValue("@users_phone", u.Phone);
                    myCommand.Parameters.AddWithValue("@users_address", u.Address);
                    myCommand.Parameters.AddWithValue("@users_email", u.Email);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();

                }
            }
            return new JsonResult("Update successfully");
        }


        [HttpDelete("delete")]
        public JsonResult Delete(string name)
        {
            string query = @"delete from dbo.tblUser
                             where users_name = @users_name"
                             ;

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("EventAppConn");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@users_name", name);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();

                }
            }
            return new JsonResult("delete successfully");
        }

    }
}
