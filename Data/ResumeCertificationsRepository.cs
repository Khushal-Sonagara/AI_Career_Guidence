namespace AI_Career_Guidence.Models;
using Microsoft.Data.SqlClient;
using System.Data;

public class ResumeCertificationsRepository
{
    private readonly string _connectionString;

    public ResumeCertificationsRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection");
    }

    #region SelectAll
    public IEnumerable<ResumeCertificationModel> SelectAll()
    {
        var resumeCertifications = new List<ResumeCertificationModel>();
        using (SqlConnection conn = new SqlConnection(_connectionString))
        {
            SqlCommand cmd = new SqlCommand("PR_Resume_ResumeCertifications_SelectAll", conn)
            {
                CommandType = System.Data.CommandType.StoredProcedure
            };
            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                resumeCertifications.Add(new ResumeCertificationModel
                {
                    CertificationID = Convert.ToInt32(reader["CertificationID"]),
                    ResumeID = Convert.ToInt32(reader["ResumeID"]),
                    CertificationName = reader["CertificationName"].ToString(),
                    IssuingOrganization = reader["IssuingOrganization"].ToString(),
                    IssueDate = reader["IssueDate"] != DBNull.Value ? Convert.ToDateTime(reader["IssueDate"]) : (DateTime?)null,
                    ExpirationDate = reader["ExpirationDate"] != DBNull.Value ? Convert.ToDateTime(reader["ExpirationDate"]) : (DateTime?)null,
                    CredentialID = reader["CredentialID"].ToString(),
                    CredentialURL = reader["CredentialURL"].ToString(),
                    CreatedAt = Convert.ToDateTime(reader["CreatedAt"]),
                    UpdatedAt = reader["UpdatedAt"] != DBNull.Value ? Convert.ToDateTime(reader["UpdatedAt"]) : (DateTime?)null
                });
            }
        }
        return resumeCertifications;
    }
    #endregion

    #region SelectByResumeId
    public IEnumerable<ResumeCertificationModel> SelectByResumeId(int resumeID)
    {
        var resumeCertifications = new List<ResumeCertificationModel>();
        using (SqlConnection conn = new SqlConnection(_connectionString))
        {
            SqlCommand cmd = new SqlCommand("PR_Resume_ResumeCertifications_SelectByResumeId", conn)
            {
                CommandType = System.Data.CommandType.StoredProcedure
            };
            cmd.Parameters.AddWithValue("@ResumeID", resumeID);
            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                resumeCertifications.Add(new ResumeCertificationModel
                {
                    CertificationID = Convert.ToInt32(reader["CertificationID"]),
                    ResumeID = Convert.ToInt32(reader["ResumeID"]),
                    CertificationName = reader["CertificationName"].ToString(),
                    IssuingOrganization = reader["IssuingOrganization"].ToString(),
                    IssueDate = reader["IssueDate"] != DBNull.Value ? Convert.ToDateTime(reader["IssueDate"]) : (DateTime?)null,
                    ExpirationDate = reader["ExpirationDate"] != DBNull.Value ? Convert.ToDateTime(reader["ExpirationDate"]) : (DateTime?)null,
                    CredentialID = reader["CredentialID"].ToString(),
                    CredentialURL = reader["CredentialURL"].ToString(),
                    CreatedAt = Convert.ToDateTime(reader["CreatedAt"]),
                    UpdatedAt = reader["UpdatedAt"] != DBNull.Value ? Convert.ToDateTime(reader["UpdatedAt"]) : (DateTime?)null
                });
            }
        }
        return resumeCertifications;
    }
    #endregion

    #region SelectByPk
    public ResumeCertificationModel SelectByPk(int certificationID)
    {
        ResumeCertificationModel resumeCertification = null;
        using (SqlConnection conn = new SqlConnection(_connectionString))
        {
            SqlCommand cmd = new SqlCommand("PR_Resume_ResumeCertifications_SelectByPk", conn)
            {
                CommandType = System.Data.CommandType.StoredProcedure
            };
            cmd.Parameters.AddWithValue("@CertificationID", certificationID);
            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                resumeCertification = new ResumeCertificationModel
                {
                    CertificationID = Convert.ToInt32(reader["CertificationID"]),
                    ResumeID = Convert.ToInt32(reader["ResumeID"]),
                    CertificationName = reader["CertificationName"].ToString(),
                    IssuingOrganization = reader["IssuingOrganization"].ToString(),
                    IssueDate = reader["IssueDate"] != DBNull.Value ? Convert.ToDateTime(reader["IssueDate"]) : (DateTime?)null,
                    ExpirationDate = reader["ExpirationDate"] != DBNull.Value ? Convert.ToDateTime(reader["ExpirationDate"]) : (DateTime?)null,
                    CredentialID = reader["CredentialID"].ToString(),
                    CredentialURL = reader["CredentialURL"].ToString(),
                    CreatedAt = Convert.ToDateTime(reader["CreatedAt"]),
                    UpdatedAt = reader["UpdatedAt"] != DBNull.Value ? Convert.ToDateTime(reader["UpdatedAt"]) : (DateTime?)null
                };
            }
        }
        return resumeCertification;
    }
    #endregion

    #region Delete
    public bool Delete(int certificationID)
    {
        using (SqlConnection conn = new SqlConnection(_connectionString))
        {
            SqlCommand cmd = new SqlCommand("PR_Resume_ResumeCertifications_Delete", conn)
            {
                CommandType = System.Data.CommandType.StoredProcedure
            };
            cmd.Parameters.AddWithValue("@CertificationID", certificationID);
            conn.Open();
            int rowsAffected = cmd.ExecuteNonQuery();
            return rowsAffected > 0;
        }
    }
    #endregion

    #region AddResumeCertification
    public bool AddResumeCertification(ResumeCertificationModel resumeCertificationModel)
    {
        int effect = 0;
        try
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("PR_Resume_ResumeCertifications_Insert", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@ResumeID", resumeCertificationModel.ResumeID);
                cmd.Parameters.AddWithValue("@CertificationName", resumeCertificationModel.CertificationName);
                cmd.Parameters.AddWithValue("@IssuingOrganization", resumeCertificationModel.IssuingOrganization ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@IssueDate", resumeCertificationModel.IssueDate ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@ExpirationDate", resumeCertificationModel.ExpirationDate ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@CredentialID", resumeCertificationModel.CredentialID ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@CredentialURL", resumeCertificationModel.CredentialURL ?? (object)DBNull.Value);
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

    #region UpdateResumeCertification
    public bool UpdateResumeCertification(ResumeCertificationModel resumeCertificationModel)
    {
        int effect = 0;
        try
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("PR_Resume_ResumeCertifications_Update", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@CertificationID", resumeCertificationModel.CertificationID);
                cmd.Parameters.AddWithValue("@ResumeID", resumeCertificationModel.ResumeID);
                cmd.Parameters.AddWithValue("@CertificationName", resumeCertificationModel.CertificationName);
                cmd.Parameters.AddWithValue("@IssuingOrganization", resumeCertificationModel.IssuingOrganization ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@IssueDate", resumeCertificationModel.IssueDate ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@ExpirationDate", resumeCertificationModel.ExpirationDate ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@CredentialID", resumeCertificationModel.CredentialID ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@CredentialURL", resumeCertificationModel.CredentialURL ?? (object)DBNull.Value);
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