using AI_Career_Guidence.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace AI_Career_Guidence.Data
{
    public class ResumeProjectsRepository
    {
        private readonly string _connectionString;

        public ResumeProjectsRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        #region SelectAll
        public IEnumerable<ResumeProjectModel> SelectAll()
        {
            var resumeProjects = new List<ResumeProjectModel>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("PR_Resume_ResumeProjects_SelectAll", conn)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    resumeProjects.Add(new ResumeProjectModel
                    {
                        ProjectID = Convert.ToInt32(reader["ProjectID"]),
                        ResumeID = Convert.ToInt32(reader["ResumeID"]),
                        Title = reader["Title"].ToString(),
                        Description = reader["Description"].ToString(),
                        TechnologiesUsed = reader["TechnologiesUsed"].ToString(),
                        StartDate = reader["StartDate"] != DBNull.Value ? Convert.ToDateTime(reader["StartDate"]) : (DateTime?)null,
                        EndDate = reader["EndDate"] != DBNull.Value ? Convert.ToDateTime(reader["EndDate"]) : (DateTime?)null,
                        ProjectURL = reader["ProjectURL"].ToString(),
                        CreatedAt = Convert.ToDateTime(reader["CreatedAt"]),
                        UpdatedAt = reader["UpdatedAt"] != DBNull.Value ? Convert.ToDateTime(reader["UpdatedAt"]) : (DateTime?)null
                    });
                }
            }
            return resumeProjects;
        }
        #endregion

        #region SelectByResumeId
        public IEnumerable<ResumeProjectModel> SelectByResumeId(int resumeID)
        {
            var resumeProjects = new List<ResumeProjectModel>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("PR_Resume_ResumeProjects_SelectByResumeId", conn)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@ResumeID", resumeID);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    resumeProjects.Add(new ResumeProjectModel
                    {
                        ProjectID = Convert.ToInt32(reader["ProjectID"]),
                        ResumeID = Convert.ToInt32(reader["ResumeID"]),
                        Title = reader["Title"].ToString(),
                        Description = reader["Description"].ToString(),
                        TechnologiesUsed = reader["TechnologiesUsed"].ToString(),
                        StartDate = reader["StartDate"] != DBNull.Value ? Convert.ToDateTime(reader["StartDate"]) : (DateTime?)null,
                        EndDate = reader["EndDate"] != DBNull.Value ? Convert.ToDateTime(reader["EndDate"]) : (DateTime?)null,
                        ProjectURL = reader["ProjectURL"].ToString(),
                        CreatedAt = Convert.ToDateTime(reader["CreatedAt"]),
                        UpdatedAt = reader["UpdatedAt"] != DBNull.Value ? Convert.ToDateTime(reader["UpdatedAt"]) : (DateTime?)null
                    });
                }
            }
            return resumeProjects;
        }
        #endregion

        #region SelectByPk
        public ResumeProjectModel SelectByPk(int projectID)
        {
            ResumeProjectModel resumeProject = null;
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("PR_Resume_ResumeProjects_SelectByPk", conn)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@ProjectID", projectID);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    resumeProject = new ResumeProjectModel
                    {
                        ProjectID = Convert.ToInt32(reader["ProjectID"]),
                        ResumeID = Convert.ToInt32(reader["ResumeID"]),
                        Title = reader["Title"].ToString(),
                        Description = reader["Description"].ToString(),
                        TechnologiesUsed = reader["TechnologiesUsed"].ToString(),
                        StartDate = reader["StartDate"] != DBNull.Value ? Convert.ToDateTime(reader["StartDate"]) : (DateTime?)null,
                        EndDate = reader["EndDate"] != DBNull.Value ? Convert.ToDateTime(reader["EndDate"]) : (DateTime?)null,
                        ProjectURL = reader["ProjectURL"].ToString(),
                        CreatedAt = Convert.ToDateTime(reader["CreatedAt"]),
                        UpdatedAt = reader["UpdatedAt"] != DBNull.Value ? Convert.ToDateTime(reader["UpdatedAt"]) : (DateTime?)null
                    };
                }
            }
            return resumeProject;
        }
        #endregion

        #region Delete
        public bool Delete(int projectID)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("PR_Resume_ResumeProjects_Delete", conn)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@ProjectID", projectID);
                conn.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }
        #endregion

        #region AddResumeProject
        public bool AddResumeProject(ResumeProjectModel resumeProjectModel)
        {
            int effect = 0;
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("PR_Resume_ResumeProjects_Insert", conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.AddWithValue("@ResumeID", resumeProjectModel.ResumeID);
                    cmd.Parameters.AddWithValue("@Title", resumeProjectModel.Title);
                    cmd.Parameters.AddWithValue("@Description", resumeProjectModel.Description);
                    cmd.Parameters.AddWithValue("@TechnologiesUsed", resumeProjectModel.TechnologiesUsed);
                    cmd.Parameters.AddWithValue("@StartDate", resumeProjectModel.StartDate ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@EndDate", resumeProjectModel.EndDate ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@ProjectURL", resumeProjectModel.ProjectURL ?? (object)DBNull.Value);
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

        #region UpdateResumeProject
        public bool UpdateResumeProject(ResumeProjectModel resumeProjectModel)
        {
            int effect = 0;
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("PR_Resume_ResumeProjects_Update", conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.AddWithValue("@ProjectID", resumeProjectModel.ProjectID);
                    cmd.Parameters.AddWithValue("@ResumeID", resumeProjectModel.ResumeID);
                    cmd.Parameters.AddWithValue("@Title", resumeProjectModel.Title);
                    cmd.Parameters.AddWithValue("@Description", resumeProjectModel.Description);
                    cmd.Parameters.AddWithValue("@TechnologiesUsed", resumeProjectModel.TechnologiesUsed);
                    cmd.Parameters.AddWithValue("@StartDate", resumeProjectModel.StartDate ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@EndDate", resumeProjectModel.EndDate ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@ProjectURL", resumeProjectModel.ProjectURL ?? (object)DBNull.Value);
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
