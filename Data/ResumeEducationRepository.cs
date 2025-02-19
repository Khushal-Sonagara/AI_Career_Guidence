using AI_Career_Guidence.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace AI_Career_Guidence.Data
{
    public class ResumeEducationRepository
    {
        private readonly string _connectionString;

        public ResumeEducationRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        #region SelectAll
        public IEnumerable<ResumeEducationModel> SelectAll()
        {
            var resumeEducations = new List<ResumeEducationModel>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("PR_Resume_ResumeEducation_SelectAll", conn)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    resumeEducations.Add(new ResumeEducationModel
                    {
                        EducationID = Convert.ToInt32(reader["EducationID"]),
                        ResumeID = Convert.ToInt32(reader["ResumeID"]),
                        UniversityOrSchoolName = reader["UniversityOrSchoolName"].ToString(),
                        Degree = reader["Degree"].ToString(),
                        Major = reader["Major"].ToString(),
                        StartDate = reader["StartDate"] != DBNull.Value ? Convert.ToDateTime(reader["StartDate"]) : (DateTime?)null,
                        EndDate = reader["EndDate"] != DBNull.Value ? Convert.ToDateTime(reader["EndDate"]) : (DateTime?)null,
                        Description = reader["Description"]?.ToString(),
                        CreatedAt = Convert.ToDateTime(reader["CreatedAt"]),
                        UpdatedAt = reader["UpdatedAt"] != DBNull.Value ? Convert.ToDateTime(reader["UpdatedAt"]) : (DateTime?)null
                    });
                }
            }
            return resumeEducations;
        }
        #endregion

        #region SelectByPk
        public ResumeEducationModel SelectByPk(int educationID)
        {
            ResumeEducationModel resumeEducation = null;
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("PR_Resume_ResumeEducation_SelectByPk", conn)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@EducationID", educationID);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    resumeEducation = new ResumeEducationModel
                    {
                        EducationID = Convert.ToInt32(reader["EducationID"]),
                        ResumeID = Convert.ToInt32(reader["ResumeID"]),
                        UniversityOrSchoolName = reader["UniversityOrSchoolName"].ToString(),
                        Degree = reader["Degree"].ToString(),
                        Major = reader["Major"].ToString(),
                        StartDate = reader["StartDate"] != DBNull.Value ? Convert.ToDateTime(reader["StartDate"]) : (DateTime?)null,
                        EndDate = reader["EndDate"] != DBNull.Value ? Convert.ToDateTime(reader["EndDate"]) : (DateTime?)null,
                        Description = reader["Description"]?.ToString(),
                        CreatedAt = Convert.ToDateTime(reader["CreatedAt"]),
                        UpdatedAt = reader["UpdatedAt"] != DBNull.Value ? Convert.ToDateTime(reader["UpdatedAt"]) : (DateTime?)null
                    };
                }
            }
            return resumeEducation;
        }
        #endregion

        #region Delete
        public bool Delete(int educationID)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("PR_Resume_ResumeEducation_Delete", conn)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@EducationID", educationID);
                conn.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }
        #endregion

        #region AddResumeEducation
        public bool AddResumeEducation(ResumeEducationModel resumeEducationModel)
        {
            int effect = 0;
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("PR_Resume_ResumeEducation_Insert", conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.AddWithValue("@ResumeID", resumeEducationModel.ResumeID);
                    cmd.Parameters.AddWithValue("@UniversityOrSchoolName", resumeEducationModel.UniversityOrSchoolName);
                    cmd.Parameters.AddWithValue("@Degree", resumeEducationModel.Degree);
                    cmd.Parameters.AddWithValue("@Major", resumeEducationModel.Major);
                    cmd.Parameters.AddWithValue("@StartDate", resumeEducationModel.StartDate ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@EndDate", resumeEducationModel.EndDate ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Description", resumeEducationModel.Description ?? (object)DBNull.Value);
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

        #region UpdateResumeEducation
        public bool UpdateResumeEducation(ResumeEducationModel resumeEducationModel)
        {
            int effect = 0;
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("PR_Resume_ResumeEducation_Update", conn)
                    {   
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.AddWithValue("@EducationID", resumeEducationModel.EducationID);
                    cmd.Parameters.AddWithValue("@ResumeID", resumeEducationModel.ResumeID);
                    cmd.Parameters.AddWithValue("@UniversityOrSchoolName", resumeEducationModel.UniversityOrSchoolName);
                    cmd.Parameters.AddWithValue("@Degree", resumeEducationModel.Degree);
                    cmd.Parameters.AddWithValue("@Major", resumeEducationModel.Major);
                    cmd.Parameters.AddWithValue("@StartDate", resumeEducationModel.StartDate ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@EndDate", resumeEducationModel.EndDate ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Description", resumeEducationModel.Description ?? (object)DBNull.Value);
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
        public IEnumerable<ResumeEducationModel> SelectByResumeId(int resumeID)
        {
            var resumeEducations = new List<ResumeEducationModel>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("PR_Resume_ResumeEducation_SelectByResumeId", conn)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@ResumeID", resumeID);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    resumeEducations.Add(new ResumeEducationModel
                    {
                        ResumeID = Convert.ToInt32(reader["ResumeID"]),
                        EducationID = Convert.ToInt32(reader["EducationID"]),
                        UniversityOrSchoolName = reader["UniversityOrSchoolName"].ToString(),
                        Degree = reader["Degree"].ToString(),
                        Major = reader["Major"].ToString(),
                        StartDate = reader["StartDate"] != DBNull.Value ? Convert.ToDateTime(reader["StartDate"]) : (DateTime?)null,
                        EndDate = reader["EndDate"] != DBNull.Value ? Convert.ToDateTime(reader["EndDate"]) : (DateTime?)null,
                        Description = reader["Description"]?.ToString(),
                        CreatedAt = Convert.ToDateTime(reader["CreatedAt"]),
                        UpdatedAt = reader["UpdatedAt"] != DBNull.Value ? Convert.ToDateTime(reader["UpdatedAt"]) : (DateTime?)null
                    });
                }
            }
            return resumeEducations;
        }
        #endregion
    }
}
