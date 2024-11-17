using System.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using QuizzyAPI.Models;

namespace QuizzyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GetUserId : ControllerBase
    {
        public readonly IConfiguration _configuration;

        public GetUserId(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // Pobierz UserID na podstawie loginu
        [HttpGet]
        [Route("GetUserIdByLogin")]
        public IActionResult GetUserIdByLogin(string login)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("ConnString")))
                {
                    string query = "SELECT UserID FROM Users WHERE Login COLLATE SQL_Latin1_General_CP1_CS_AS = @Login";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@Login", login);

                        con.Open();

                        object result = cmd.ExecuteScalar();

                        if (result != null)
                        {
                            int userId = Convert.ToInt32(result);
                            return Ok(userId);
                        }
                        else
                        {
                            return NotFound("User with this login doesn't exist.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // Pobierz UserID na podstawie emaila
        [HttpGet]
        [Route("GetUserIdByEmail")]
        public IActionResult GetUserIdByEmail(string email)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("ConnString")))
                {
                    string query = "SELECT UserID FROM Users WHERE Email COLLATE SQL_Latin1_General_CP1_CS_AS = @Email";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@Email", email);

                        con.Open();

                        object result = cmd.ExecuteScalar();

                        if (result != null)
                        {
                            int userId = Convert.ToInt32(result);
                            return Ok(userId);
                        }
                        else
                        {
                            return NotFound("User with this email doesn't exist.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
