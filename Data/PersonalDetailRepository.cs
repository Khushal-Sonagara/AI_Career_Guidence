using AI_Career_Guidence.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace AI_Career_Guidence.Data
{
    public class PersonalDetailRepository
    {
        private readonly string _connectionString;

        public PersonalDetailRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        #region SelectAll
        public IEnumerable<PersonalDetailModel> SelectAll()
        {
            var personalDetails = new List<PersonalDetailModel>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("PR_Resume_PersonalDetails_SelectAll", conn)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    personalDetails.Add(new PersonalDetailModel
                    {
                        PersonalDetailID = Convert.ToInt32(reader["PersonalDetailID"]),
                        ResumeID = Convert.ToInt32(reader["ResumeID"]),
                        FirstName = reader["FirstName"].ToString(),
                        LastName = reader["LastName"].ToString(),
                        JobTitle = reader["JobTitle"].ToString(),
                        Address = reader["Address"].ToString(),
                        PhoneNumber = reader["PhoneNumber"].ToString(),
                        Email = reader["Email"].ToString(),
                        LinkedIn = reader["LinkedIn"]?.ToString(),
                        CreatedAt = reader["CreatedAt"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(reader["CreatedAt"]),
                        UpdatedAt = reader["UpdatedAt"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(reader["UpdatedAt"])
                    });
                }
            }
            return personalDetails;
        }
        #endregion

        #region SelectByPk
        public PersonalDetailModel SelectByPk(int personalDetailID)
        {
            PersonalDetailModel personalDetail = null;
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("PR_Resume_PersonalDetails_SelectByPk", conn)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@PersonalDetailID", personalDetailID);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    personalDetail = new PersonalDetailModel
                    {
                        PersonalDetailID = Convert.ToInt32(reader["PersonalDetailID"]),
                        ResumeID = Convert.ToInt32(reader["ResumeID"]),
                        FirstName = reader["FirstName"].ToString(),
                        LastName = reader["LastName"].ToString(),
                        JobTitle = reader["JobTitle"].ToString(),
                        Address = reader["Address"].ToString(),
                        PhoneNumber = reader["PhoneNumber"].ToString(),
                        Email = reader["Email"].ToString(),
                        LinkedIn = reader["LinkedIn"]?.ToString(),
                        CreatedAt = reader["CreatedAt"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(reader["CreatedAt"]),
                        UpdatedAt = reader["UpdatedAt"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(reader["UpdatedAt"])
                    };
                }
            }
            return personalDetail;
        }
        #endregion

        #region Delete
        public bool Delete(int personalDetailID)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("PR_Resume_PersonalDetails_Delete", conn)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@PersonalDetailID", personalDetailID);
                conn.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }
        #endregion

        #region AddPersonalDetail
        public bool AddPersonalDetail(PersonalDetailModel personalDetailModel)
        {
            int effect = 0;
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("PR_Resume_PersonalDetails_Insert", conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.AddWithValue("@ResumeID", personalDetailModel.ResumeID);
                    cmd.Parameters.AddWithValue("@FirstName", personalDetailModel.FirstName);
                    cmd.Parameters.AddWithValue("@LastName", personalDetailModel.LastName);
                    cmd.Parameters.AddWithValue("@JobTitle", personalDetailModel.JobTitle);
                    cmd.Parameters.AddWithValue("@Address", personalDetailModel.Address);
                    cmd.Parameters.AddWithValue("@PhoneNumber", personalDetailModel.PhoneNumber);
                    cmd.Parameters.AddWithValue("@Email", personalDetailModel.Email);
                    cmd.Parameters.AddWithValue("@LinkedIn", personalDetailModel.LinkedIn ?? (object)DBNull.Value);
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

        #region UpdatePersonalDetail
        public bool UpdatePersonalDetail(PersonalDetailModel personalDetailModel)
        {
            int effect = 0;
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("PR_Resume_PersonalDetails_Update", conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.AddWithValue("@PersonalDetailID", personalDetailModel.PersonalDetailID);
                    cmd.Parameters.AddWithValue("@ResumeID", personalDetailModel.ResumeID);
                    cmd.Parameters.AddWithValue("@FirstName", personalDetailModel.FirstName);
                    cmd.Parameters.AddWithValue("@LastName", personalDetailModel.LastName);
                    cmd.Parameters.AddWithValue("@JobTitle", personalDetailModel.JobTitle);
                    cmd.Parameters.AddWithValue("@Address", personalDetailModel.Address);
                    cmd.Parameters.AddWithValue("@PhoneNumber", personalDetailModel.PhoneNumber);
                    cmd.Parameters.AddWithValue("@Email", personalDetailModel.Email);
                    cmd.Parameters.AddWithValue("@LinkedIn", personalDetailModel.LinkedIn ?? (object)DBNull.Value);
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
            public IEnumerable<PersonalDetailModel> SelectByResumeId(int resumeID)
            {
                var personalDetailsList = new List<PersonalDetailModel>();
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("PR_Resume_PersonalDetails_SelectByResumeId", conn)
                    {
                        CommandType = System.Data.CommandType.StoredProcedure
                    };
                    cmd.Parameters.AddWithValue("@ResumeID", resumeID);
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        personalDetailsList.Add(new PersonalDetailModel
                        {
                            PersonalDetailID = Convert.ToInt32(reader["PersonalDetailID"]),
                            ResumeID = Convert.ToInt32(reader["ResumeID"]),
                            FirstName = reader["FirstName"].ToString(),
                            LastName = reader["LastName"].ToString(),
                            JobTitle = reader["JobTitle"].ToString(),
                            Address = reader["Address"].ToString(),
                            PhoneNumber = reader["PhoneNumber"].ToString(),
                            Email = reader["Email"].ToString(),
                            LinkedIn = reader["LinkedIn"]?.ToString(),
                            CreatedAt = reader["CreatedAt"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(reader["CreatedAt"]),
                            UpdatedAt = reader["UpdatedAt"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(reader["UpdatedAt"])
                        });
                    }
                }
                return personalDetailsList;
            }
            #endregion
        }
    }
