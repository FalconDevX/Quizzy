using System.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using QuizzyAPI.Models;

namespace QuizzyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IsTaken : ControllerBase
    {
        public readonly IConfiguration _configuration;
        public IsTaken(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        [HttpGet]
        [Route("IsEmailTaken")]
        public bool GetEmail(string email)
        {
            SqlConnection con = new SqlConnection(_configuration.GetConnectionString("ConnString").ToString());
            SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM Users", con);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (Convert.ToString(dt.Rows[i]["Email"]) == email)
                    {
                        return true;
                    }
                }
                return false;
            }
            else
            {
                return false;
            }
        }
        [HttpGet]
        [Route("IsLoginTaken")]
        public bool GetLogin(string login)
        {
            SqlConnection con = new SqlConnection(_configuration.GetConnectionString("ConnString").ToString());
            SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM Users", con);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (Convert.ToString(dt.Rows[i]["Login"]) == login)
                    {
                        return true;
                    }
                }
                return false;
            }
            else
            {
                return false;
            }
        }
    }
}
