using AI_Career_Guidence.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace AI_Career_Guidence.Data
{
    public class ResumeExperienceRepository
    {
        private readonly string _connectionString;

        public ResumeExperienceRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        #region SelectAll
        public IEnumerable<ResumeExperienceModel> SelectAll()
        {
            var resumeExperiences = new List<ResumeExperienceModel>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("PR_Resume_ResumeExperience_SelectAll", conn)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    resumeExperiences.Add(new ResumeExperienceModel
                    {
                        ExperienceID = Convert.ToInt32(reader["ExperienceID"]),
                        ResumeID = Convert.ToInt32(reader["ResumeID"]),
                        Title = reader["Title"].ToString(),
                        CompanyName = reader["CompanyName"].ToString(),
                        City = reader["City"].ToString(),
                        State = reader["State"].ToString(),
                        StartDate = reader["StartDate"] != DBNull.Value ? Convert.ToDateTime(reader["StartDate"]) : (DateTime?)null,
                        EndDate = reader["EndDate"] != DBNull.Value ? Convert.ToDateTime(reader["EndDate"]) : (DateTime?)null,
                        WorkSummary = reader["WorkSummary"]?.ToString(),
                        CreatedAt = reader["CreatedAt"] != DBNull.Value ? Convert.ToDateTime(reader["CreatedAt"]) : (DateTime?)null,
                        UpdatedAt = reader["UpdatedAt"] != DBNull.Value ? Convert.ToDateTime(reader["UpdatedAt"]) : (DateTime?)null
                    });
                }
            }
            return resumeExperiences;
        }
        #endregion

        #region SelectByPk
        public ResumeExperienceModel SelectByPk(int experienceID)
        {
            ResumeExperienceModel resumeExperience = null;
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("PR_Resume_ResumeExperience_SelectByPk", conn)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@ExperienceID", experienceID);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    resumeExperience = new ResumeExperienceModel
                    {
                        ExperienceID = Convert.ToInt32(reader["ExperienceID"]),
                        ResumeID = Convert.ToInt32(reader["ResumeID"]),
                        Title = reader["Title"].ToString(),
                        CompanyName = reader["CompanyName"].ToString(),
                        City = reader["City"].ToString(),
                        State = reader["State"].ToString(),
                        StartDate = reader["StartDate"] != DBNull.Value ? Convert.ToDateTime(reader["StartDate"]) : (DateTime?)null,
                        EndDate = reader["EndDate"] != DBNull.Value ? Convert.ToDateTime(reader["EndDate"]) : (DateTime?)null,
                        WorkSummary = reader["WorkSummary"]?.ToString(),
                        CreatedAt = Convert.ToDateTime(reader["CreatedAt"]),
                        UpdatedAt = reader["UpdatedAt"] != DBNull.Value ? Convert.ToDateTime(reader["UpdatedAt"]) : (DateTime?)null
                    };
                }
            }
            return resumeExperience;
        }
        #endregion

        #region Delete
        public bool Delete(int experienceID)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("PR_Resume_ResumeExperience_Delete", conn)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@ExperienceID", experienceID);
                conn.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }
        #endregion

        #region AddResumeExperience
        public bool AddResumeExperience(ResumeExperienceModel resumeExperienceModel)
        {
            int effect = 0;
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("PR_Resume_ResumeExperience_Insert", conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.AddWithValue("@ResumeID", resumeExperienceModel.ResumeID);
                    cmd.Parameters.AddWithValue("@Title", resumeExperienceModel.Title);
                    cmd.Parameters.AddWithValue("@CompanyName", resumeExperienceModel.CompanyName);
                    cmd.Parameters.AddWithValue("@City", resumeExperienceModel.City);
                    cmd.Parameters.AddWithValue("@State", resumeExperienceModel.State);
                    cmd.Parameters.AddWithValue("@StartDate", resumeExperienceModel.StartDate ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@EndDate", resumeExperienceModel.EndDate ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@WorkSummary", resumeExperienceModel.WorkSummary ?? (object)DBNull.Value);
                    conn.Open();
                    effect = cmd.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return effect > 0;
        }
        #endregion

        #region UpdateResumeExperience
        public bool UpdateResumeExperience(ResumeExperienceModel resumeExperienceModel)
        {
            int effect = 0;
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("PR_Resume_ResumeExperience_Update", conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.AddWithValue("@ExperienceID", resumeExperienceModel.ExperienceID);
                    cmd.Parameters.AddWithValue("@ResumeID", resumeExperienceModel.ResumeID);
                    cmd.Parameters.AddWithValue("@Title", resumeExperienceModel.Title);
                    cmd.Parameters.AddWithValue("@CompanyName", resumeExperienceModel.CompanyName);
                    cmd.Parameters.AddWithValue("@City", resumeExperienceModel.City);
                    cmd.Parameters.AddWithValue("@State", resumeExperienceModel.State);
                    cmd.Parameters.AddWithValue("@StartDate", resumeExperienceModel.StartDate ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@EndDate", resumeExperienceModel.EndDate ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@WorkSummary", resumeExperienceModel.WorkSummary ?? (object)DBNull.Value);
                    conn.Open();
                    effect = cmd.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return effect > 0;
        }
        #endregion
        #region SelectByResumeId
        public IEnumerable<ResumeExperienceModel> SelectByResumeId(int resumeID)
        {
            var resumeExperiences = new List<ResumeExperienceModel>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("PR_Resume_ResumeExperiences_SelectByResumeId", conn)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@ResumeID", resumeID);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    resumeExperiences.Add(new ResumeExperienceModel
                    {
                        ExperienceID = Convert.ToInt32(reader["ExperienceID"]),
                        ResumeID = Convert.ToInt32(reader["ResumeID"]),
                        Title = reader["Title"].ToString(),
                        CompanyName = reader["CompanyName"].ToString(),
                        City = reader["City"].ToString(),
                        State = reader["State"].ToString(),
                        StartDate = reader["StartDate"] != DBNull.Value ? Convert.ToDateTime(reader["StartDate"]) : (DateTime?)null,
                        EndDate = reader["EndDate"] != DBNull.Value ? Convert.ToDateTime(reader["EndDate"]) : (DateTime?)null,
                        WorkSummary = reader["WorkSummary"]?.ToString(),
                        CreatedAt = Convert.ToDateTime(reader["CreatedAt"]),
                        UpdatedAt = reader["UpdatedAt"] != DBNull.Value ? Convert.ToDateTime(reader["UpdatedAt"]) : (DateTime?)null
                    });
                }
            }
            return resumeExperiences;
        }
        #endregion
    }
}
