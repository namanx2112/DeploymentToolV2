using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using DeploymentTool.Model;

namespace DeploymentTool.Helpers
{
    public class AttachmentDAL
    {
        private readonly string _connectionString = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;

        public int CreateAttachment(Attachment attachment)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (var command = new SqlCommand("sprocAttachmentCreate", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@tfileName", attachment.FileName);
                    command.Parameters.AddWithValue("@tFileExt", attachment.FileExt);
                    command.Parameters.AddWithValue("@tfileType", attachment.FileType);
                    command.Parameters.AddWithValue("@tAttachmentType", attachment.AttachmentType);
                    command.Parameters.AddWithValue("@tAttachmentComments", attachment.AttachmentComments);
                    command.Parameters.AddWithValue("@tAttachmentBlob", attachment.AttachmentBlob);
                    command.Parameters.AddWithValue("@tAttachmentURL", attachment.AttachmentUrl);
                    command.Parameters.AddWithValue("@nUserID", attachment.CreatedBy);

                    var attachmentIdParameter = new SqlParameter("@nAttachmentId", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };
                    command.Parameters.Add(attachmentIdParameter);

                    command.ExecuteNonQuery();

                    return Convert.ToInt32(attachmentIdParameter.Value);
                }
            }
        }


        public bool UpdateAttachment(Attachment attachment)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("sprocAttachmentUpdate", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                command.Parameters.AddWithValue("@nAttachmentId", attachment.AttachmentId);
                command.Parameters.AddWithValue("@tfileName", attachment.FileName);
                command.Parameters.AddWithValue("@tFileExt", attachment.FileExt);
                command.Parameters.AddWithValue("@tfileType", attachment.FileType);
                command.Parameters.AddWithValue("@tAttachmentType", attachment.AttachmentType);
                command.Parameters.AddWithValue("@tAttachmentComments", attachment.AttachmentComments);
                command.Parameters.AddWithValue("@tAttachmentBlob", attachment.AttachmentBlob);
                command.Parameters.AddWithValue("@tAttachmentURL", attachment.AttachmentUrl);
                command.Parameters.AddWithValue("@nUserID", attachment.UpdateBy);

                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();

                return rowsAffected > 0;
            }
        }

        public void DeleteAttachment(int attachmentId, int userId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (var command = new SqlCommand("sprocAttachmentDelete", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@nAttachmentId", attachmentId);
                    command.Parameters.AddWithValue("@nUserID", userId);

                    command.ExecuteNonQuery();
                }
            }
        }


        public List<Attachment> GetAttachments(int? attachmentId, int? userId, string fileName, int pageSize = 10, int pageNumber = 1)

        {
            var attachments = new List<Attachment>();
            using (var conn = new SqlConnection(_connectionString))
            {
                using (var cmd = new SqlCommand("sprocAttachmentGet", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@nAttachmentId", attachmentId.HasValue ? attachmentId.Value : (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@nUserID", userId.HasValue ? userId.Value : (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@tFileName", !string.IsNullOrEmpty(fileName) ? fileName : (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@nPageSize", pageSize);
                    cmd.Parameters.AddWithValue("@nPageNumber", pageNumber);

                    conn.Open();

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var attachment = new Attachment
                            {
                                AttachmentId = Convert.ToInt32(reader["aAttachmentId"]),
                                FileName = Convert.ToString(reader["tFileName"]),
                                FileExt = Convert.ToString(reader["tFileExt"]),
                                FileType = Convert.ToString(reader["tFileType"]),
                                AttachmentType = Convert.ToString(reader["tAttachmentType"]),
                                AttachmentComments = Convert.ToString(reader["tAttachmentComments"]),
                                AttachmentUrl = Convert.ToString(reader["tAttachmentURL"]),
                                //AttachmentBlob = (byte[])reader["tAttachmentBlob"],
                            };
                            attachments.Add(attachment);

                        }
                    }
                }
            }
            return attachments;
        }


        public Attachment GetAttachmentBlob(int attachmentId, int userId)
        {
            Attachment attachment = null;

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sprocAttachmentBlobGet", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@nAttachmentId", attachmentId);
                    cmd.Parameters.AddWithValue("@nUserID", userId);

                    conn.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            attachment = new Attachment()
                            {
                                AttachmentId = reader.GetInt32(reader.GetOrdinal("aAttachmentId")),
                                FileName = reader.GetString(reader.GetOrdinal("tfileName")),
                                FileType = reader.GetString(reader.GetOrdinal("tfileType")),
                                FileExt = reader.GetString(reader.GetOrdinal("tFileExt")),
                                AttachmentBlob = (byte[])reader["tAttachmentBlob"]
                            };
                        }
                    }
                }
            }

            return attachment;
        }
    }
}

