using System.Data;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using QuizzyAPI.Models;

namespace QuizzyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        public readonly IConfiguration _configuration;
        public ValuesController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        [HttpGet]
        [Route("GetAllUsers")]
        public string Get()
        {
            SqlConnection con = new SqlConnection(_configuration.GetConnectionString("ConnString").ToString());
            SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM Users", con);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            List<User> users = new List<User>();
            Resp response = new Resp();
            if (dt.Rows.Count > 0) {
                for(int i = 0; i < dt.Rows.Count; i++)
                {
                    User user = new User();
                    user.UserLogin = Convert.ToString(dt.Rows[i]["Login"]);
                    user.Id = Convert.ToInt32(dt.Rows[i]["UserID"]);
                    user.UserMail = Convert.ToString(dt.Rows[i]["Email"]);
                    users.Add(user);
                }
            }
            if (users.Count > 0)
            {
                return JsonConvert.SerializeObject(users);
            }
            else
            {
                response.Status = 100;
                response.Message = "NOTOK";
                return JsonConvert.SerializeObject(response);
            }
        }
    }
}
