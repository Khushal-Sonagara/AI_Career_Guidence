using AI_Career_Guidence.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace AI_Career_Guidence.Data
{
    public class UserRepository
    {
        private readonly string _connectionString;

        public UserRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        #region Insert
        public UserModel Insert(UserModel user)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("PR_User_Insert", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@Username", user.Username);
                cmd.Parameters.AddWithValue("@Email", user.Email);
                cmd.Parameters.AddWithValue("@ClerkUserId", user.ClerkUserId);

                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new UserModel
                        {
                            UserID = Convert.ToInt32(reader["Id"]),
                            Username = reader["Username"].ToString(),
                            Email = reader["Email"].ToString(),
                            ClerkUserId = reader["ClerkUserId"].ToString(),
                            CreatedAt = Convert.ToDateTime(reader["CreatedAt"])
                        };
                    }
                }
            }
            return null;
        }

        #endregion

        #region GetByClerkUserId
        public UserModel GetByClerkUserId(string clerkUserId)
        {
            UserModel user = null;

            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("PR_User_SelectByClerkUserId", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ClerkUserId", clerkUserId);

                        conn.Open();

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                user = new UserModel
                                {
                                    UserID = Convert.ToInt32(reader["UserID"]),
                                    Username = reader["Username"].ToString(),
                                    Email = reader["Email"].ToString(),
                                    ClerkUserId = reader["ClerkUserId"].ToString(),
                                    CreatedAt = reader["CreatedAt"] != DBNull.Value? Convert.ToDateTime(reader["CreatedAt"]): (DateTime?)null
                                };
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in GetByClerkUserId: " + ex.Message);
            }

            return user;
        }

        #endregion
    }
}
