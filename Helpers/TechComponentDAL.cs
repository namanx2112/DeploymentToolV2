using DeploymentTool.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace DeploymentTool.Helpers
{
    public class TechComponentDAL
    {

        public int CreateTechComponent(TechComponent techcomponent, int nUserId)
        {
            int techcomponentId = 0;
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@tTechComponentName", techcomponent.tTechComponentName),
                    new SqlParameter("@nBrandID", techcomponent.nBrandID),
                    new SqlParameter("@tTechComponentDescription", techcomponent.tTechComponentDescription),
                    new SqlParameter("@tComponentType", techcomponent.tComponentType),                   
                    new SqlParameter("@nUserId", nUserId)
                };
                SqlParameter[] outputParams = new SqlParameter[]
                {
            new SqlParameter("@nTechComponentID", SqlDbType.Int)
            {
                Direction = ParameterDirection.Output
            }
                };
               
                DBHelper.ExecuteProcedure("sprocTechComponentCreate", ref outputParams, parameters);
                techcomponentId = (int)outputParams[0].Value;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return techcomponentId;
        }
        public List<TechComponent> GetTechComponents(TechComponent inputtechcomponent, int nUserID)
        {
            SqlParameter[] parameters = null;
            if (inputtechcomponent != null)
            {
                parameters = new SqlParameter[]
                {

                new SqlParameter("@nTechComponentID", inputtechcomponent.aTechComponentId),
                new SqlParameter("@nBrandID", inputtechcomponent.nBrandID)
                };
            }
            else
            {
                parameters = new SqlParameter[] { new SqlParameter("@nUserID", nUserID) };
            }

            return DBHelper.ExecuteProcedure<List<TechComponent>>("sprocTechComponentGet", reader =>
            {
                var techComponents = new List<TechComponent>();

                while (reader.Read())
                {
                    var techComponent = new TechComponent();

                    techComponent.aTechComponentId = reader["aTechComponentId"] == null ? -1 : (int)reader["aTechComponentId"];
                    techComponent.tTechComponentName = reader["tTechComponentName"]?.ToString();
                    techComponent.nBrandID = reader["nBrandID"] == null ? -1 : (int)reader["nBrandID"];
                    techComponent.tTechComponentDescription = reader["tTechComponentDescription"]?.ToString();
                    techComponent.tComponentType = reader["tComponentType"]?.ToString();
                    techComponent.nCreatedBy = reader["nCreatedBy"] is DBNull ? 0 : (int)reader["nCreatedBy"];
                    techComponent.nUpdateBy = reader["nUpdateBy"] is DBNull ? 0 : (int)reader["nUpdateBy"];
                    techComponent.dtCreatedOn = reader["dtCreatedOn"] is DBNull ? DateTime.MinValue : (DateTime)reader["dtCreatedOn"];
                    techComponent.dtUpdatedOn = reader["dtUpdatedOn"] is DBNull ? DateTime.MinValue : (DateTime)reader["dtUpdatedOn"];
                    techComponent.bDeleted = (bool)reader["bDeleted"];
                    techComponents.Add(techComponent);
                }

                return techComponents;
            }, parameters);
        }
        public void Update(TechComponent techcomponent)
        {
            var parameters = new SqlParameter[]
            {
                new SqlParameter("@nTechComponentID", techcomponent.aTechComponentId),
                new SqlParameter("@tTechComponentName", techcomponent.tTechComponentName),
                new SqlParameter("@nBrandID", techcomponent.nBrandID),
                new SqlParameter("@tTechComponentDescription", techcomponent.tTechComponentDescription),
                new SqlParameter("@tComponentType", techcomponent.tComponentType),
                new SqlParameter("@nUserId", techcomponent.nUpdateBy)
            };

            DBHelper.ExecuteNonQuery("sprocTechComponentUpdate", parameters);
        }
        public void Delete(TechComponent techcomponent, int userId)
        {
            var parameters = new SqlParameter[]
            {
                new SqlParameter("@nTechComponentID", techcomponent.aTechComponentId),
                new SqlParameter("@nUserID", userId)
            };
            DBHelper.ExecuteNonQuery("sprocTechComponentDelete", parameters);
        }
    }
}