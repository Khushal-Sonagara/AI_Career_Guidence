using AI_Career_Guidence.Models;
using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;

namespace AI_Career_Guidence.Data
{
    public class UserResumeRepository
    {
        private readonly string _connectionString;
        public UserResumeRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        #region SelectAll
        public IEnumerable<UserResumeModel> SelectAll()
        {
            var resumes = new List<UserResumeModel>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("PR_Resume_UserResume_SelectAll", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    resumes.Add(new UserResumeModel
                    {
                        ResumeID = Convert.ToInt32(reader["ResumeID"]),
                        UserID = Convert.ToInt32(reader["UserID"]),
                        ResumeTitle = reader["ResumeTitle"].ToString(),
                        CreatedAt = Convert.ToDateTime(reader["CreatedAt"]),
                        UpdatedAt = reader["UpdatedAt"] as DateTime?
                    });
                }
            }
            return resumes;
        }
        #endregion
        #region SelectByUserId
        public IEnumerable<UserResumeModel> SelectByUserId(int userID)
        {
            var resumes = new List<UserResumeModel>(); // List to store multiple resumes

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("PR_Resume_UserResume_SelectByUserID", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@UserID", userID);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read()) // Read multiple records
                {
                    resumes.Add(new UserResumeModel
                    {
                        ResumeID = Convert.ToInt32(reader["ResumeID"]),
                        UserID = Convert.ToInt32(reader["UserID"]),
                        ResumeTitle = reader["ResumeTitle"].ToString(),
                        CreatedAt = Convert.ToDateTime(reader["CreatedAt"]),
                        UpdatedAt = reader["UpdatedAt"] as DateTime?
                    });
                }
            }
            return resumes;
        }
        #endregion


        #region Insert
        public bool Insert(UserResumeModel resume)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("PR_Resume_UserResume_Insert", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@UserID", resume.UserID);
                cmd.Parameters.AddWithValue("@ResumeTitle", resume.ResumeTitle);
                conn.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }
        #endregion

        #region Update
        public bool Update(UserResumeModel resume)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("PR_Resume_UserResume_Update", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@ResumeID", resume.ResumeID);
                cmd.Parameters.AddWithValue("@UserID", resume.UserID);
                cmd.Parameters.AddWithValue("@ResumeTitle", resume.ResumeTitle);
                conn.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }
        #endregion

        public async Task<bool> UpdateResumeImageId(int resumeId, int? resumeImageId)
        {
            using var connection = new SqlConnection(_connectionString);
            try
            {
                var affectedRows = await connection.ExecuteAsync(
                    "EXEC PR_Resume_UserResume_UpdateResumeImageID @ResumeID, @ResumeImageID",
                    new { ResumeID = resumeId, ResumeImageID = resumeImageId }
                );

                if (affectedRows == 0)
                {
                    Console.WriteLine($"Update failed: ResumeID {resumeId} not found or no changes made.");
                }
                Console.WriteLine($"Affected Rows: {affectedRows}");

                return affectedRows > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"SQL Update Error: {ex.Message}");
                return false;
            }
        }




        #region Delete
        public bool Delete(int resumeID)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("PR_Resume_UserResume_Delete", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@ResumeID", resumeID);
                conn.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }
        #endregion

        #region SelectByResumeID
        public UserResumeModel SelectByResumeID(int resumeID)
        {
            UserResumeModel resume = null;
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("PR_Resume_UserResume_SelectByPk", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@ResumeID", resumeID);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    resume = new UserResumeModel
                    {
                        ResumeID = Convert.ToInt32(reader["ResumeID"]),
                        UserID = Convert.ToInt32(reader["UserID"]),
                        ResumeTitle = reader["ResumeTitle"].ToString(),
                        CreatedAt = Convert.ToDateTime(reader["CreatedAt"]),
                        UpdatedAt = reader["UpdatedAt"] as DateTime?
                    };
                }
            }
            return resume;
        }
        #endregion
    }
}
