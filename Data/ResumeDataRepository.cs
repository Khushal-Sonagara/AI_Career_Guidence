using System;
using System.Data;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using AI_Career_Guidence.Models;

namespace AI_Career_Guidence.Data
{
    public class ResumeDataRepository
    {
        private readonly string _connectionString;

        public ResumeDataRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public ResumeDataModel GetResumeDataByResumeId(int resumeID)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (var multi = conn.QueryMultiple("PR_GetResumeDataByResumeId",
                                                      new { ResumeID = resumeID },
                                                      commandType: CommandType.StoredProcedure))
                {
                    var resumeData = new ResumeDataModel
                    {
                        Resume = multi.ReadFirstOrDefault<UserResumeModel>(),
                        PersonalDetails = multi.ReadFirstOrDefault<PersonalDetailModel>(),
                        ResumeSummary = multi.ReadFirstOrDefault<ResumeSummaryModel>(),
                        ResumeExperiences = multi.Read<ResumeExperienceModel>().AsList(),
                        ResumeEducation = multi.Read<ResumeEducationModel>().AsList(),
                        ResumeSkills = multi.Read<ResumeSkillModel>().AsList(),
                        ResumeProjects = multi.Read<ResumeProjectModel>().AsList(),
                        ResumeCertifications = multi.Read<ResumeCertificationModel>().AsList(),
                        ResumeLanguages = multi.Read<ResumeLanguageModel>().AsList(),
                        ResumeInterests = multi.Read<ResumeInterestModel>().AsList(),
                        ResumePhoto = multi.ReadFirstOrDefault<ResumePhotoModel>()
                    };

                    return resumeData;
                }
            }
        }

        public bool DeleteResumeByResumeId(int resumeID)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                int rowsAffected = conn.ExecuteScalar<int>(
                    "PR_Resume_DeleteByResumeId",
                    new { ResumeID = resumeID },
                    commandType: CommandType.StoredProcedure
                );

                return rowsAffected > 0; // Return true if any row was affected
            }
        }

    }
}
