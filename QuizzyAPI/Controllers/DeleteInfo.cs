using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace QuizzyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeleteInfo : ControllerBase
    {
        public readonly IConfiguration _configuration;
        public DeleteInfo(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        [HttpDelete]
        [Route("DeleteUser")]
        public IActionResult DeleteUser(int id)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("ConnString")))
                {
                    string query = "DELETE FROM Users WHERE UserID = @id";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@id", id);

                        con.Open();

                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            return Ok("User deleted successfully.");
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
