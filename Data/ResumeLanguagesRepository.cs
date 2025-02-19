using AI_Career_Guidence.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace AI_Career_Guidence.Data
{
    public class ResumeLanguagesRepository
    {
        private readonly string _connectionString;

        public ResumeLanguagesRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        #region SelectAll
        public IEnumerable<ResumeLanguageModel> SelectAll()
        {
            var resumeLanguages = new List<ResumeLanguageModel>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("PR_Resume_ResumeLanguages_SelectAll", conn)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    resumeLanguages.Add(new ResumeLanguageModel
                    {
                        LanguageID = Convert.ToInt32(reader["LanguageID"]),
                        ResumeID = Convert.ToInt32(reader["ResumeID"]),
                        Language = reader["Language"].ToString(),
                        ProficiencyLevel = reader["ProficiencyLevel"].ToString(),
                        CreatedAt = Convert.ToDateTime(reader["CreatedAt"]),
                        UpdatedAt = reader["UpdatedAt"] != DBNull.Value ? Convert.ToDateTime(reader["UpdatedAt"]) : (DateTime?)null
                    });
                }
            }
            return resumeLanguages;
        }
        #endregion

        #region SelectByResumeId
        public IEnumerable<ResumeLanguageModel> SelectByResumeId(int resumeID)
        {
            var resumeLanguages = new List<ResumeLanguageModel>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("PR_Resume_ResumeLanguages_SelectByResumeId", conn)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@ResumeID", resumeID);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    resumeLanguages.Add(new ResumeLanguageModel
                    {
                        LanguageID = Convert.ToInt32(reader["LanguageID"]),
                        ResumeID = Convert.ToInt32(reader["ResumeID"]),
                        Language = reader["Language"].ToString(),
                        ProficiencyLevel = reader["ProficiencyLevel"].ToString(),
                        CreatedAt = Convert.ToDateTime(reader["CreatedAt"]),
                        UpdatedAt = reader["UpdatedAt"] != DBNull.Value ? Convert.ToDateTime(reader["UpdatedAt"]) : (DateTime?)null
                    });
                }
            }
            return resumeLanguages;
        }
        #endregion

        #region SelectByPk
        public ResumeLanguageModel SelectByPk(int languageID)
        {
            ResumeLanguageModel resumeLanguage = null;
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("PR_Resume_ResumeLanguages_SelectByPk", conn)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@LanguageID", languageID);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    resumeLanguage = new ResumeLanguageModel
                    {
                        LanguageID = Convert.ToInt32(reader["LanguageID"]),
                        ResumeID = Convert.ToInt32(reader["ResumeID"]),
                        Language = reader["Language"].ToString(),
                        ProficiencyLevel = reader["ProficiencyLevel"].ToString(),
                        CreatedAt = Convert.ToDateTime(reader["CreatedAt"]),
                        UpdatedAt = reader["UpdatedAt"] != DBNull.Value ? Convert.ToDateTime(reader["UpdatedAt"]) : (DateTime?)null
                    };
                }
            }
            return resumeLanguage;
        }
        #endregion

        #region Delete
        public bool Delete(int languageID)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("PR_Resume_ResumeLanguages_Delete", conn)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@LanguageID", languageID);
                conn.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }
        #endregion

        #region AddResumeLanguage
        public bool AddResumeLanguage(ResumeLanguageModel resumeLanguageModel)
        {
            int effect = 0;
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("PR_Resume_ResumeLanguages_Insert", conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.AddWithValue("@ResumeID", resumeLanguageModel.ResumeID);
                    cmd.Parameters.AddWithValue("@Language", resumeLanguageModel.Language);
                    cmd.Parameters.AddWithValue("@ProficiencyLevel", resumeLanguageModel.ProficiencyLevel);
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

        #region UpdateResumeLanguage
        public bool UpdateResumeLanguage(ResumeLanguageModel resumeLanguageModel)
        {
            int effect = 0;
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("PR_Resume_ResumeLanguages_Update", conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.AddWithValue("@LanguageID", resumeLanguageModel.LanguageID);
                    cmd.Parameters.AddWithValue("@ResumeID", resumeLanguageModel.ResumeID);
                    cmd.Parameters.AddWithValue("@Language", resumeLanguageModel.Language);
                    cmd.Parameters.AddWithValue("@ProficiencyLevel", resumeLanguageModel.ProficiencyLevel);
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
    }

}