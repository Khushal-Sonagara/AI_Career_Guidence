using AI_Career_Guidence.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace AI_Career_Guidence.Data
{
    public class ResumeInterestsRepository
    {
        private readonly string _connectionString;

        public ResumeInterestsRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        #region SelectAll
        public IEnumerable<ResumeInterestModel> SelectAll()
        {
            var resumeInterests = new List<ResumeInterestModel>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("PR_Resume_ResumeInterests_SelectAll", conn)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    resumeInterests.Add(new ResumeInterestModel
                    {
                        InterestID = Convert.ToInt32(reader["InterestID"]),
                        ResumeID = Convert.ToInt32(reader["ResumeID"]),
                        Interest = reader["Interest"].ToString(),
                        CreatedAt = Convert.ToDateTime(reader["CreatedAt"]),
                        UpdatedAt = reader["UpdatedAt"] != DBNull.Value ? Convert.ToDateTime(reader["UpdatedAt"]) : (DateTime?)null
                    });
                }
            }
            return resumeInterests;
        }
        #endregion

        #region SelectByResumeId
        public IEnumerable<ResumeInterestModel> SelectByResumeId(int resumeID)
        {
            var resumeInterests = new List<ResumeInterestModel>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("PR_Resume_ResumeInterests_SelectByResumeId", conn)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@ResumeID", resumeID);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    resumeInterests.Add(new ResumeInterestModel
                    {
                        InterestID = Convert.ToInt32(reader["InterestID"]),
                        ResumeID = Convert.ToInt32(reader["ResumeID"]),
                        Interest = reader["Interest"].ToString(),
                        CreatedAt = Convert.ToDateTime(reader["CreatedAt"]),
                        UpdatedAt = reader["UpdatedAt"] != DBNull.Value ? Convert.ToDateTime(reader["UpdatedAt"]) : (DateTime?)null
                    });
                }
            }
            return resumeInterests;
        }
        #endregion

        #region SelectByPk
        public ResumeInterestModel SelectByPk(int interestID)
        {
            ResumeInterestModel resumeInterest = null;
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("PR_Resume_ResumeInterests_SelectByPk", conn)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@InterestID", interestID);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    resumeInterest = new ResumeInterestModel
                    {
                        InterestID = Convert.ToInt32(reader["InterestID"]),
                        ResumeID = Convert.ToInt32(reader["ResumeID"]),
                        Interest = reader["Interest"].ToString(),
                        CreatedAt = Convert.ToDateTime(reader["CreatedAt"]),
                        UpdatedAt = reader["UpdatedAt"] != DBNull.Value ? Convert.ToDateTime(reader["UpdatedAt"]) : (DateTime?)null
                    };
                }
            }
            return resumeInterest;
        }
        #endregion

        #region Delete
        public bool Delete(int interestID)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("PR_Resume_ResumeInterests_Delete", conn)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@InterestID", interestID);
                conn.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }
        #endregion

        #region AddResumeInterest
        public bool AddResumeInterest(ResumeInterestModel resumeInterestModel)
        {
            int effect = 0;
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("PR_Resume_ResumeInterests_Insert", conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.AddWithValue("@ResumeID", resumeInterestModel.ResumeID);
                    cmd.Parameters.AddWithValue("@Interest", resumeInterestModel.Interest);
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

        #region UpdateResumeInterest
        public bool UpdateResumeInterest(ResumeInterestModel resumeInterestModel)
        {
            int effect = 0;
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("PR_Resume_ResumeInterests_Update", conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.AddWithValue("@InterestID", resumeInterestModel.InterestID);
                    cmd.Parameters.AddWithValue("@ResumeID", resumeInterestModel.ResumeID);
                    cmd.Parameters.AddWithValue("@Interest", resumeInterestModel.Interest);
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