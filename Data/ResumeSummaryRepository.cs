using AI_Career_Guidence.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace AI_Career_Guidence.Data
{
    public class ResumeSummaryRepository
    {
        private readonly string _connectionString;

        public ResumeSummaryRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        #region SelectAll
        public IEnumerable<ResumeSummaryModel> SelectAll()
        {
            var summaries = new List<ResumeSummaryModel>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("PR_Resume_ResumeSummaries_SelectAll", conn)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    summaries.Add(new ResumeSummaryModel
                    {
                        SummaryID = Convert.ToInt32(reader["SummaryID"]),
                        ResumeID = Convert.ToInt32(reader["ResumeID"]),
                        SummaryText = reader["SummaryText"].ToString(),
                        CreatedAt = Convert.ToDateTime(reader["CreatedAt"]),
                        UpdatedAt = reader["UpdatedAt"] == DBNull.Value ? null : Convert.ToDateTime(reader["UpdatedAt"])
                    });
                }
            }
            return summaries;
        }
        #endregion

        #region SelectByPk
        public ResumeSummaryModel SelectByPk(int summaryID)
        {
            ResumeSummaryModel summary = null;
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("PR_Resume_ResumeSummaries_SelectByPk", conn)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@SummaryID", summaryID);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    summary = new ResumeSummaryModel
                    {
                        SummaryID = Convert.ToInt32(reader["SummaryID"]),
                        ResumeID = Convert.ToInt32(reader["ResumeID"]),
                        SummaryText = reader["SummaryText"].ToString(),
                        CreatedAt = Convert.ToDateTime(reader["CreatedAt"]),
                        UpdatedAt = reader["UpdatedAt"] == DBNull.Value ? null : Convert.ToDateTime(reader["UpdatedAt"])
                    };
                }
            }
            return summary;
        }
        #endregion

        #region Delete
        public bool Delete(int summaryID)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("PR_Resume_ResumeSummaries_Delete", conn)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@SummaryID", summaryID);
                conn.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }
        #endregion

        #region AddResumeSummary
        public bool AddResumeSummary(ResumeSummaryModel summaryModel)
        {
            int effect = 0;
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("PR_Resume_ResumeSummaries_Insert", conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.AddWithValue("@ResumeID", summaryModel.ResumeID);
                    cmd.Parameters.AddWithValue("@SummaryText", summaryModel.SummaryText);
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

        #region UpdateResumeSummary
        public bool UpdateResumeSummary(ResumeSummaryModel summaryModel)
        {
            int effect = 0;
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("PR_Resume_ResumeSummaries_Update", conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.AddWithValue("@SummaryID", summaryModel.SummaryID);
                    cmd.Parameters.AddWithValue("@ResumeID", summaryModel.ResumeID);
                    cmd.Parameters.AddWithValue("@SummaryText", summaryModel.SummaryText);
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
        public IEnumerable<ResumeSummaryModel> SelectByResumeId(int resumeID)
        {
            var resumeSummaries = new List<ResumeSummaryModel>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("PR_Resume_ResumeSummaries_SelectByResumeId", conn)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@ResumeID", resumeID);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    resumeSummaries.Add(new ResumeSummaryModel
                    {
                        SummaryID = Convert.ToInt32(reader["SummaryID"]),
                        ResumeID = Convert.ToInt32(reader["ResumeID"]),
                        SummaryText = reader["SummaryText"].ToString(),
                        CreatedAt = Convert.ToDateTime(reader["CreatedAt"]),
                        UpdatedAt = reader["UpdatedAt"] != DBNull.Value ? Convert.ToDateTime(reader["UpdatedAt"]) : (DateTime?)null
                    });
                }
            }
            return resumeSummaries;
        }
        #endregion
    }
}
