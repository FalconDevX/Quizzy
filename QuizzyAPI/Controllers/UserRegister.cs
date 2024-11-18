﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using QuizzyAPI.Models;

namespace QuizzyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterUserController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public RegisterUserController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost]
        [Route("Register")]
        public IActionResult Register(string login, string email, string password)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("ConnString")))
                {
                    string query = @"
                        INSERT INTO Users (Login, Email, PasswordHash, Avatar) 
                        VALUES (@Login, @Email, @PasswordHash, NULL);
                        SELECT SCOPE_IDENTITY();";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@Login", login);
                        cmd.Parameters.AddWithValue("@Email", email);
                        cmd.Parameters.AddWithValue("@PasswordHash", password);

                        con.Open();

                        object result = cmd.ExecuteScalar();

                        if (result != null)
                        {
                            int userId = Convert.ToInt32(result);
                            return Ok(new { message = "User registered successfully", userId = userId });
                        }
                        else
                        {
                            return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Failed to register user" });
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
