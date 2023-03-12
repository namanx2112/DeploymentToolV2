using DeploymentTool.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace DeploymentTool.Helpers
{
    public class VendorDAL
    {
        public List<Vendor> GetVendors(Vendor inputvendor, int nUserID)
        {
            SqlParameter[] parameters = null;
            if (inputvendor != null)
            {
                parameters = new SqlParameter[]
                {

                new SqlParameter("@nVendorId", inputvendor.aVendorId),
                new SqlParameter("@nBrandID", inputvendor.nBrandID),
                new SqlParameter("@nTechComponentID", inputvendor.nTechComponentID),
                new SqlParameter("@tVendorName", inputvendor.tVendorName),
                new SqlParameter("@nUserID", nUserID),
                new SqlParameter("@nPageSize", inputvendor.nPageSize),
                new SqlParameter("@nPageNumber", inputvendor.nPageNumber)
                };
            }
            else
            {
                parameters = new SqlParameter[] { new SqlParameter("@nUserID", nUserID) };
            }

            return DBHelper.ExecuteProcedure<List<Vendor>>("SprocVendorGet", reader =>
            {
                var vendors = new List<Vendor>();

                while (reader.Read())
                {
                    var vendor = new Vendor();

                    vendor.aVendorId = reader["aVendorId"] == null ? -1 : (int)reader["aVendorId"];                   
                    vendor.tVendorName = reader["tVendorName"]?.ToString();
                    vendor.nTechComponentID = reader["nTechComponentID"] == null ? -1 : (int)reader["nTechComponentID"];
                    vendor.tVendorDescription = reader["tVendorDescription"]?.ToString();
                    vendor.tVendorEmail = reader["tVendorEmail"]?.ToString();
                    vendor.tVendorAddress = reader["tVendorAddress"]?.ToString();
                    vendor.tVendorPhone = reader["tVendorPhone"]?.ToString();
                    vendor.tVendorContactPerson = reader["tVendorContactPerson"]?.ToString();
                    vendor.tVendorWebsite = reader["tVendorWebsite"]?.ToString();
                    vendor.tVendorCountry = reader["tVendorCountry"]?.ToString();                    
                    vendor.tVendorEstablished = reader["tVendorEstablished"] is DBNull ? DateTime.MinValue : (DateTime)reader["tVendorEstablished"];
                    vendor.tVendorCategory = reader["tVendorCategory"]?.ToString();
                    vendor.tVendorContact = reader["tVendorContact"]?.ToString();
                    vendor.nCreatedBy = reader["nCreatedBy"] is DBNull ? 0 : (int)reader["nCreatedBy"];
                    vendor.nBrandID = reader["aBrandID"] is DBNull ? 0 : (int)reader["aBrandID"];
                    vendor.nTechComponentID = reader["aTechComponentID"] is DBNull ? 0 : (int)reader["aTechComponentID"];
                    vendor.nUpdatedBy = reader["nUpdateBy"] is DBNull ? 0 : (int)reader["nUpdateBy"];
                    vendor.dtCreatedOn = reader["dtCreatedOn"] is DBNull ? DateTime.MinValue : (DateTime)reader["dtCreatedOn"];
                    vendor.dtUpdatedOn = reader["dtUpdatedOn"] is DBNull ? DateTime.MinValue : (DateTime)reader["dtUpdatedOn"];
                    vendor.bDeleted = (bool)reader["bDeleted"];
                    vendors.Add(vendor);
                }

                return vendors;
            }, parameters);
        }
        public int CreateVendor(Vendor vendor, int nUserId)
        {
            int vendorId = 0;
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@tVendorName", vendor.tVendorName),
                    new SqlParameter("@nTechComponentID", vendor.nTechComponentID),
                    new SqlParameter("@tVendorDescription", vendor.tVendorDescription),
                    new SqlParameter("@tVendorEmail", vendor.tVendorEmail),
                    new SqlParameter("@tVendorAddress", vendor.tVendorAddress),
                    new SqlParameter("@tVendorPhone", vendor.tVendorPhone),
                    new SqlParameter("@tVendorContactPerson", vendor.tVendorContactPerson),
                    new SqlParameter("@tVendorWebsite", vendor.tVendorWebsite),
                    new SqlParameter("@tVendorCountry", vendor.tVendorCountry),
                    new SqlParameter("@tVendorEstablished", vendor.tVendorEstablished),
                    new SqlParameter("@tVendorCategory", vendor.tVendorCategory),
                    new SqlParameter("@tVendorContact", vendor.tVendorContact),
                    new SqlParameter("@nUserId", nUserId)
                };
                SqlParameter[] outputParams = new SqlParameter[]
                {
            new SqlParameter("@nVendorID", SqlDbType.Int)
            {
                Direction = ParameterDirection.Output
            }
                };                
                DBHelper.ExecuteProcedure("sprocVendorCreate", ref outputParams, parameters);
                vendorId = (int)outputParams[0].Value;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return vendorId;
        }
        public void Update(Vendor vendor)
        {
            var parameters = new SqlParameter[]
            {
                    new SqlParameter("@nVendorId", vendor.aVendorId),
                    new SqlParameter("@tVendorName", vendor.tVendorName),
                    new SqlParameter("@nTechComponentID", vendor.nTechComponentID),
                    new SqlParameter("@tVendorDescription", vendor.tVendorDescription),
                    new SqlParameter("@tVendorEmail", vendor.tVendorEmail),
                    new SqlParameter("@tVendorAddress", vendor.tVendorAddress),
                    new SqlParameter("@tVendorPhone", vendor.tVendorPhone),
                    new SqlParameter("@tVendorContactPerson", vendor.tVendorContactPerson),
                    new SqlParameter("@tVendorWebsite", vendor.tVendorWebsite),
                    new SqlParameter("@tVendorCountry", vendor.tVendorCountry),
                    new SqlParameter("@tVendorEstablished", vendor.tVendorEstablished),
                    new SqlParameter("@tVendorCategory", vendor.tVendorCategory),
                    new SqlParameter("@tVendorContact", vendor.tVendorContact),
                    new SqlParameter("@nUserId", vendor.nUpdatedBy)
       
            };

            DBHelper.ExecuteNonQuery("sprocVendorUpdate", parameters);
        }
        public void Delete(Vendor vendor, int userId)
        {
            var parameters = new SqlParameter[]
            {
                new SqlParameter("@nVendorId", vendor.aVendorId),
                new SqlParameter("@nUserId", userId)
            };
            DBHelper.ExecuteNonQuery("sprocVendorDelete", parameters);
        }
    }
}