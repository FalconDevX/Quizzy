using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace QuizzyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChangeUserInfo : ControllerBase
    {
        public readonly IConfiguration _configuration;
        public ChangeUserInfo(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost]
        [Route("ChangeLogin")]
        public IActionResult ChangeLogin(int id, string login)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("ConnString")))
                {
                    string query = "UPDATE Users SET Login = @login WHERE UserID = @id";
                    using(SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@login", login);
                        cmd.Parameters.AddWithValue("@id", id);
                        
                        con.Open();
                        
                        int rowsAffected = cmd.ExecuteNonQuery();

                        if(rowsAffected > 0)
                        {
                            return Ok("Login Updated succesfully.");
                        }
                        else
                        {
                            return NotFound("User with the provided ID doesn't exist.");
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Database error: " + ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An error occurred: " + ex.Message });
            }
        }

        [HttpPost]
        [Route("ChangeAvatar")]
        public IActionResult ChangeAvatar(int id, byte[] avatar)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("ConnString")))
                {
                    string query = "UPDATE Users SET Avatar = @avatar WHERE UserID = @id";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@avatar", avatar);
                        cmd.Parameters.AddWithValue("@id", id);

                        con.Open();

                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            return Ok("Avatar Updated succesfully.");
                        }
                        else
                        {
                            return NotFound("User with the provided ID doesn't exist.");
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Database error: " + ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An error occurred: " + ex.Message });
            }
        }
    }
}
