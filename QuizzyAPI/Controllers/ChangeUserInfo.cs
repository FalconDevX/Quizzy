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
        [Route("ChangePasswd")]
        public IActionResult ChangePasswd(int id, string passwd)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("ConnString")))
                {
                    string query = "UPDATE Users SET PasswordHash = @newpasswd WHERE UserID = @id";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@newpasswd", passwd);
                        cmd.Parameters.AddWithValue("@id", id);

                        con.Open();

                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            return Ok("Password Updated succesfully.");
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
        [Route("ChangeAvatar")]
        [HttpPost]
        public IActionResult ChangeAvatar(int id, IFormFile avatar)
        {
            try
            {
                if (avatar == null || avatar.Length == 0)
                {
                    return BadRequest("No file was uploaded.");
                }

                using (var memoryStream = new MemoryStream())
                {
                    avatar.CopyTo(memoryStream);
                    byte[] avatarBytes = memoryStream.ToArray();

                    using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("ConnString")))
                    {
                        string query = "UPDATE Users SET Avatar = @avatar WHERE UserID = @id";
                        using (SqlCommand cmd = new SqlCommand(query, con))
                        {
                            cmd.Parameters.AddWithValue("@avatar", avatarBytes);
                            cmd.Parameters.AddWithValue("@id", id);

                            con.Open();
                            int rowsAffected = cmd.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {
                                return Ok("Avatar updated successfully.");
                            }
                            else
                            {
                                return NotFound("User with the provided ID doesn't exist.");
                            }
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
