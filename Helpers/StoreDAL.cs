using DeploymentTool.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace DeploymentTool.Helpers
{
    public class StoreDAL
    {
        public int CreateStore(Store store, int nUserId)
        {
            int storeId = 0;
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@tStoreName", store.tStoreName),
                    new SqlParameter("@nFranchiseID", store.nFranchiseID),
                    new SqlParameter("@tStoreDescription", store.tStoreDescription),
                    new SqlParameter("@tStoreLocation", store.tStoreLocation),                    
                    new SqlParameter("@tStoreEstablished", store.tStoreEstablished),
                    new SqlParameter("@tStoreContact", store.tStoreContact),
                    new SqlParameter("@tStoreManager", store.tStoreManager),
                    new SqlParameter("@tStoreEmail", store.tStoreEmail),
                    new SqlParameter("@tStorePhone", store.tStorePhone),
                    new SqlParameter("@tStoreAddress", store.tStoreAddress),
                    new SqlParameter("@tStoreHours", store.tStoreHours),
                    new SqlParameter("@tStoreWebsite", store.tStoreWebsite),
                    new SqlParameter("@tStoreServices", store.tStoreServices),
                    new SqlParameter("@tStoreSize", store.tStoreSize),
                    new SqlParameter("@tStoreParking", store.tStoreParking),
                    new SqlParameter("@tStoreAmenities", store.tStoreAmenities),
                    new SqlParameter("@tStoreTax_id", store.tStoreTax_id),
                    new SqlParameter("@tStoreSales_tax", store.tStoreSales_tax),
                    new SqlParameter("@tStorePayment_methods", store.tStorePayment_methods),
                    new SqlParameter("@tStoreDelivery", store.tStoreDelivery),
                    new SqlParameter("@tLatitude", store.tLatitude),
                    new SqlParameter("@tLongitude", store.tLongitude),                    
                    new SqlParameter("@nUserId", nUserId)
                };
                SqlParameter[] outputParams = new SqlParameter[]
                {
                    new SqlParameter("@nStoreId", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    }
                };
                DBHelper.ExecuteProcedure("sprocStoreCreate", ref outputParams, parameters);
                storeId = (int)outputParams[0].Value;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return storeId;
        }

        public void Update(Store store)
        {
            var parameters = new SqlParameter[]
            {
                    new SqlParameter("@nStoreId", store.aStoreId),
                    new SqlParameter("@tStoreName", store.tStoreName),
                    new SqlParameter("@nFranchiseID", store.nFranchiseID),
                    new SqlParameter("@tStoreDescription", store.tStoreDescription),
                    new SqlParameter("@tStoreLocation", store.tStoreLocation),
                    new SqlParameter("@tStoreEstablished", store.tStoreEstablished),
                    new SqlParameter("@tStoreContact", store.tStoreContact),
                    new SqlParameter("@tStoreManager", store.tStoreManager),
                    new SqlParameter("@tStoreEmail", store.tStoreEmail),
                    new SqlParameter("@tStorePhone", store.tStorePhone),
                    new SqlParameter("@tStoreAddress", store.tStoreAddress),
                    new SqlParameter("@tStoreHours", store.tStoreHours),
                    new SqlParameter("@tStoreWebsite", store.tStoreWebsite),
                    new SqlParameter("@tStoreServices", store.tStoreServices),
                    new SqlParameter("@tStoreSize", store.tStoreSize),
                    new SqlParameter("@tStoreParking", store.tStoreParking),
                    new SqlParameter("@tStoreAmenities", store.tStoreAmenities),
                    new SqlParameter("@tStoreTax_id", store.tStoreTax_id),
                    new SqlParameter("@tStoreSales_tax", store.tStoreSales_tax),
                    new SqlParameter("@tStorePayment_methods", store.tStorePayment_methods),
                    new SqlParameter("@tStoreDelivery", store.tStoreDelivery),
                    new SqlParameter("@tLatitude", store.tLatitude),
                    new SqlParameter("@tLongitude", store.tLongitude),
                    new SqlParameter("@nUserId", store.nUpdateBy)
            };

            DBHelper.ExecuteNonQuery("sprocStoreUpdate", parameters);
        }
        public List<Store> GetStores(Store inputstore, int nUserID)
        {
            SqlParameter[] parameters = null;
            if (inputstore != null)
            {
                parameters = new SqlParameter[]
                {

                new SqlParameter("@nStoreID", inputstore.aStoreId),
                new SqlParameter("@nUserID", nUserID),
                new SqlParameter("@tStoreName", inputstore.tStoreName)               
                };
            }
            else
            {
                parameters = new SqlParameter[] { new SqlParameter("@nUserID", nUserID) };
            }

            return DBHelper.ExecuteProcedure<List<Store>>("sprocStoreGet", reader =>
            {
                var stores = new List<Store>();

                while (reader.Read())
                {
                    var store = new Store();

                    store.aStoreId = reader["aStoreId"] == null ? -1 : (int)reader["aStoreId"];                   
                    store.tStoreName = reader["tStoreName"]?.ToString();
                    store.nFranchiseID = reader["nFranchiseID"] == null ? -1 : (int)reader["nFranchiseID"];                    
                    store.tStoreDescription = reader["tStoreDescription"]?.ToString();
                    store.tStoreLocation = reader["tStoreLocation"]?.ToString();                    
                    store.tStoreEstablished = reader["tStoreEstablished"] is DBNull ? DateTime.MinValue : (DateTime)reader["dtCreatedOn"];
                    store.tStoreContact = reader["tStoreContact"]?.ToString();
                    store.tStoreManager = reader["tStoreManager"]?.ToString();
                    store.tStoreEmail = reader["tStoreEmail"]?.ToString();
                    store.tStorePhone = reader["tStorePhone"]?.ToString();
                    store.tStoreAddress = reader["tStoreAddress"]?.ToString();
                    store.tStoreHours = reader["tStoreHours"]?.ToString();
                    store.tStoreWebsite = reader["tStoreWebsite"]?.ToString();
                    store.tStoreServices = reader["tStoreServices"]?.ToString();
                    store.tStoreSize = reader["tStoreSize"] == null ? -1 : (int)reader["tStoreSize"];                   
                    store.tStoreParking = reader["tStoreParking"]?.ToString();
                    store.tStoreAmenities = reader["tStoreAmenities"]?.ToString();
                    store.tStoreTax_id = reader["tStoreTax_id"]?.ToString();
                    store.tStoreSales_tax = reader["tStoreSales_tax"]?.ToString();
                    store.tStorePayment_methods = reader["tStorePayment_methods"]?.ToString();
                    store.tStoreDelivery = reader["tStoreDelivery"]?.ToString();
                    store.tLatitude = reader["tLatitude"]?.ToString();
                    store.tLongitude = reader["tLongitude"]?.ToString();
                    store.nCreatedBy = reader["nCreatedBy"] is DBNull ? 0 : (int)reader["nCreatedBy"];
                    store.nUpdateBy = reader["nUpdateBy"] is DBNull ? 0 : (int)reader["nUpdateBy"];
                    store.dtCreatedOn = reader["dtCreatedOn"] is DBNull ? DateTime.MinValue : (DateTime)reader["dtCreatedOn"];
                    store.dtUpdatedOn = reader["dtUpdatedOn"] is DBNull ? DateTime.MinValue : (DateTime)reader["dtUpdatedOn"];
                    store.bDeleted = (bool)reader["bDeleted"];
                    stores.Add(store);
                }

                return stores;
            }, parameters);
        }
    }
}