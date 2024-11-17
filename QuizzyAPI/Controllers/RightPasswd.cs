using System.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace QuizzyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RightPasswd : ControllerBase
    {
        public readonly IConfiguration _configuration;
        public RightPasswd(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        [HttpGet]
        [Route("UsingLogin")]
        public bool CheckWithLogin(string login, string passwd)
        {
            SqlConnection con = new SqlConnection(_configuration.GetConnectionString("ConnString").ToString());
            SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM Users", con);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            if(dt.Rows.Count > 0 )
            {
                for(int i = 0; i < dt.Rows.Count; i++)
                {
                    if (Convert.ToString(dt.Rows[i]["Login"]) == login)
                    {
                        if (Convert.ToString(dt.Rows[i]["PasswordHash"]) == passwd){
                            return true;
                        }
                        else
                        {
                            return false;
                        }
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
        [Route("UsingEmail")]
        public bool CheckWithEmail(string email, string passwd)
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
                        if (Convert.ToString(dt.Rows[i]["PasswordHash"]) == passwd)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
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
