using System.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using QuizzyAPI.Models;

namespace QuizzyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GetUserInfo : ControllerBase
    {
        public readonly IConfiguration _configuration;

        public GetUserInfo(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // Route to get email by UserID
        [HttpGet]
        [Route("GetEmailById")]
        public IActionResult GetEmailById(int id)
        {
            SqlConnection con = new SqlConnection(_configuration.GetConnectionString("ConnString").ToString());
            SqlDataAdapter adapter = new SqlDataAdapter("SELECT Email FROM [dbo].[Users] WHERE UserID = @Id", con);
            adapter.SelectCommand.Parameters.AddWithValue("@Id", id);

            DataTable dt = new DataTable();
            adapter.Fill(dt);

            if (dt.Rows.Count > 0)
            {
                string email = dt.Rows[0]["Email"].ToString();
                return Ok(email);
            }
            else
            {
                return NotFound("User with the provided ID doesn't exist.");
            }
        }

        // Route to get login by UserID
        [HttpGet]
        [Route("GetLoginById")]
        public IActionResult GetLoginById(int id)
        {
            SqlConnection con = new SqlConnection(_configuration.GetConnectionString("ConnString").ToString());
            SqlDataAdapter adapter = new SqlDataAdapter("SELECT Login FROM [dbo].[Users] WHERE UserID = @Id", con);
            adapter.SelectCommand.Parameters.AddWithValue("@Id", id);

            DataTable dt = new DataTable();
            adapter.Fill(dt);

            if (dt.Rows.Count > 0)
            {
                string login = dt.Rows[0]["Login"].ToString();
                return Ok(login);
            }
            else
            {
                return NotFound("User with the provided ID doesn't exist.");
            }
        }
    }
}
