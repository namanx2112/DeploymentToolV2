using DeploymentTool.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace DeploymentTool.Helpers
{
    public class FranchiseDAL
    {
        public void Delete(int franchiseId, int userId)
        {
            var parameters = new SqlParameter[]
            {
                new SqlParameter("@nFranchiseId", franchiseId),
                new SqlParameter("@nUserID", userId)
            };
            DBHelper.ExecuteNonQuery("sprocFranchiseDelete", parameters);
        }
        public void Update(Franchise franchise, int UserID)
        {
            var parameters = new SqlParameter[]
            {
                new SqlParameter("@nFranchiseId", franchise.aFranchiseId),
                new SqlParameter("@tFranchiseName", franchise.tFranchiseName),
                new SqlParameter("@nBrandID", franchise.nBrandId),
                new SqlParameter("@tFranchiseDescription", franchise.tFranchiseDescription),
                new SqlParameter("@tFranchiseLocation", franchise.tFranchiseLocation),
                new SqlParameter("@dFranchiseEstablished", franchise.dFranchiseEstablished),
                new SqlParameter("@tFranchiseContact", franchise.tFranchiseContact),
                new SqlParameter("@tFranchiseOwner", franchise.tFranchiseOwner),
                new SqlParameter("@tFranchiseEmail", franchise.tFranchiseEmail),
                new SqlParameter("@tFranchisePhone", franchise.tFranchisePhone),
                new SqlParameter("@tFranchiseAddress", franchise.tFranchiseAddress),
                new SqlParameter("@nFranchiseEmployeeCount", franchise.nFranchiseEmployeeCount),
                new SqlParameter("@nFranchiseRevenue", franchise.nFranchiseRevenue),
                new SqlParameter("@nUserID",UserID)               
                
            };

            DBHelper.ExecuteNonQuery("sprocFranchiseUpdate", parameters);
        }
        public int CreateFranchise(Franchise franchise, int UserID)
        {
            int franchiseID = 0;
            string connectionString = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@tFranchiseName", franchise.tFranchiseName),
                    new SqlParameter("@nBrandID", franchise.nBrandId),
                    new SqlParameter("@tFranchiseDescription", franchise.tFranchiseDescription),
                    new SqlParameter("@tFranchiseLocation", franchise.tFranchiseLocation),
                    new SqlParameter("@dFranchiseEstablished", franchise.dFranchiseEstablished),
                    new SqlParameter("@tFranchiseContact", franchise.tFranchiseContact),
                    new SqlParameter("@tFranchiseOwner", franchise.tFranchiseOwner),
                    new SqlParameter("@tFranchiseEmail", franchise.tFranchiseEmail),
                    new SqlParameter("@tFranchisePhone", franchise.tFranchisePhone),
                    new SqlParameter("@tFranchiseAddress", franchise.tFranchiseAddress),
                    new SqlParameter("@nFranchiseEmployeeCount", franchise.nFranchiseEmployeeCount),
                    new SqlParameter("@nFranchiseRevenue", franchise.nFranchiseRevenue),
                    new SqlParameter("@nUserID",UserID)
                };
                SqlParameter[] outputParams = new SqlParameter[]
                {
                new SqlParameter("@nFranchiseID", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Output
                }
                };
                
                DBHelper.ExecuteProcedure("sprocFranchiseCreate", ref outputParams, parameters);
                franchiseID = (int)outputParams[0].Value;


            }
            catch (Exception ex)
            {
                throw ex;
            }
            return franchiseID;
        }
        public List<Franchise> GetFranchises(Franchise objFranchises, int UserID)
        {
            var parameters = new SqlParameter[]
            {
                new SqlParameter("@nFranchiseId", objFranchises.aFranchiseId),
                new SqlParameter("@tFranchiseName", objFranchises.tFranchiseName),
                new SqlParameter("@nBrandID", objFranchises.nBrandId),
                new SqlParameter("@tFranchiseEmail", objFranchises.tFranchiseEmail),
                new SqlParameter("@tFranchiseOwner", objFranchises.tFranchiseOwner),
                new SqlParameter("@tFranchisePhone", objFranchises.tFranchisePhone),               
                new SqlParameter("@nUserID", UserID),
                new SqlParameter("@nPageSize", objFranchises.nPageSize),
                new SqlParameter("@nPageNumber", objFranchises.nPageNumber)
            };

            return DBHelper.ExecuteProcedure<List<Franchise>>("sprocFranchiseGet", reader =>
            {
                var franchises = new List<Franchise>();

                while (reader.Read())
                {
                    var franchise = new Franchise();

                    franchise.aFranchiseId = reader["aFranchiseId"] == null ? -1 : (int)reader["aFranchiseId"];
                    franchise.tFranchiseName = reader["tFranchiseName"]?.ToString();
                    franchise.nBrandId = reader["nBrandID"] == null ? -1 : (int)reader["nBrandID"];
                    franchise.tFranchiseDescription = reader["tFranchiseDescription"]?.ToString();
                    franchise.tFranchiseLocation = reader["tFranchiseLocation"]?.ToString();
                    franchise.dFranchiseEstablished = reader["dFranchiseEstablished"] is DBNull ? DateTime.MinValue : (DateTime)reader["dFranchiseEstablished"];
                    franchise.tFranchiseContact = reader["tFranchiseContact"]?.ToString();
                    franchise.tFranchiseOwner = reader["tFranchiseOwner"]?.ToString();
                    franchise.tFranchiseEmail = reader["tFranchiseEmail"]?.ToString();
                    franchise.tFranchisePhone = reader["tFranchisePhone"]?.ToString();
                    franchise.tFranchiseAddress = reader["tFranchiseAddress"]?.ToString();
                    franchise.nFranchiseEmployeeCount = reader["nFranchiseEmployeeCount"] is DBNull ? 0 : (int)reader["nFranchiseEmployeeCount"];
                    franchise.nFranchiseRevenue = reader["nFranchiseRevenue"] is DBNull ? 0 : (int)reader["nFranchiseRevenue"];
                    franchise.nCreatedBy = reader["nCreatedBy"] is DBNull ? 0 : (int)reader["nCreatedBy"];
                    franchise.nUpdateBy = reader["nUpdateBy"] is DBNull ? 0 : (int)reader["nUpdateBy"];
                    franchise.dtCreatedOn = reader["dtCreatedOn"] is DBNull ? DateTime.MinValue : (DateTime)reader["dtCreatedOn"];
                    franchise.dtUpdatedOn = reader["dtUpdatedOn"] is DBNull ? DateTime.MinValue : (DateTime)reader["dtUpdatedOn"];
                    franchise.bDeleted = (bool)reader["bDeleted"];
                    franchises.Add(franchise);
                }

                return franchises;
            }, parameters);
        }
    }
}