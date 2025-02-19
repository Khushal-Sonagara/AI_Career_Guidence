using AI_Career_Guidence.Models;
using Microsoft.Data.SqlClient;

namespace AI_Career_Guidence.Data
{
    public class ResumeSkillsRepository
    {
        private readonly string _connectionString;

        public ResumeSkillsRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }


        #region SelectAll
        public IEnumerable<ResumeSkillModel> SelectAll()
        {
            var resumeSkills = new List<ResumeSkillModel>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("PR_Resume_ResumeSkills_SelectAll", conn)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    resumeSkills.Add(new ResumeSkillModel
                    {
                        ResumeSkillID = Convert.ToInt32(reader["ResumeSkillID"]),
                        ResumeID = Convert.ToInt32(reader["ResumeID"]),
                        SkillName = reader["SkillName"].ToString(),
                        Rating = Convert.ToInt32(reader["Rating"]),
                        CreatedAt = Convert.ToDateTime(reader["CreatedAt"]),
                        UpdatedAt = reader["UpdatedAt"] != DBNull.Value ? Convert.ToDateTime(reader["UpdatedAt"]) : (DateTime?)null
                    });
                }
            }
            return resumeSkills;
        }
        #endregion

        #region SelectByResumeId
        public IEnumerable<ResumeSkillModel> SelectByResumeId(int resumeID)
        {
            var resumeSkills = new List<ResumeSkillModel>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("PR_Resume_ResumeSkills_SelectByResumeId", conn)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@ResumeID", resumeID);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    resumeSkills.Add(new ResumeSkillModel
                    {
                        ResumeSkillID = Convert.ToInt32(reader["ResumeSkillID"]),
                        ResumeID = Convert.ToInt32(reader["ResumeID"]),
                        SkillName = reader["SkillName"].ToString(),
                        Rating = Convert.ToInt32(reader["Rating"]),
                        CreatedAt = Convert.ToDateTime(reader["CreatedAt"]),
                        UpdatedAt = reader["UpdatedAt"] != DBNull.Value ? Convert.ToDateTime(reader["UpdatedAt"]) : (DateTime?)null
                    });
                }
            }
            return resumeSkills;
        }
        #endregion

        #region SelectByPk
        public ResumeSkillModel SelectByPk(int resumeSkillID)
        {
            ResumeSkillModel resumeSkill = null;
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("PR_Resume_ResumeSkills_SelectByPk", conn)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@ResumeSkillID", resumeSkillID);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    resumeSkill = new ResumeSkillModel
                    {
                        ResumeSkillID = Convert.ToInt32(reader["ResumeSkillID"]),
                        ResumeID = Convert.ToInt32(reader["ResumeID"]),
                        SkillName = reader["SkillName"].ToString(),
                        Rating = Convert.ToInt32(reader["Rating"]),
                        CreatedAt = Convert.ToDateTime(reader["CreatedAt"]),
                        UpdatedAt = reader["UpdatedAt"] != DBNull.Value ? Convert.ToDateTime(reader["UpdatedAt"]) : (DateTime?)null
                    };
                }
            }
            return resumeSkill;
        }
        #endregion

        #region Delete
        public bool Delete(int resumeSkillID)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("PR_Resume_ResumeSkills_Delete", conn)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@ResumeSkillID", resumeSkillID);
                conn.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }
        #endregion

        #region AddResumeSkill
        public bool AddResumeSkill(ResumeSkillModel resumeSkillModel)
        {
            int effect = 0;
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("PR_Resume_ResumeSkills_Insert", conn)
                    {
                        CommandType = System.Data.CommandType.StoredProcedure
                    };
                    cmd.Parameters.AddWithValue("@ResumeID", resumeSkillModel.ResumeID);
                    cmd.Parameters.AddWithValue("@SkillName", resumeSkillModel.SkillName);
                    cmd.Parameters.AddWithValue("@Rating", resumeSkillModel.Rating);
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

        #region UpdateResumeSkill
        public bool UpdateResumeSkill(ResumeSkillModel resumeSkillModel)
        {
            int effect = 0;
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("PR_Resume_ResumeSkills_Update", conn)
                    {
                        CommandType = System.Data.CommandType.StoredProcedure
                    };
                    cmd.Parameters.AddWithValue("@ResumeSkillID", resumeSkillModel.ResumeSkillID);
                    cmd.Parameters.AddWithValue("@ResumeID", resumeSkillModel.ResumeID);
                    cmd.Parameters.AddWithValue("@SkillName", resumeSkillModel.SkillName);
                    cmd.Parameters.AddWithValue("@Rating", resumeSkillModel.Rating);
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
