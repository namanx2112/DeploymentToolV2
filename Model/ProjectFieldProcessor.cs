using DeploymentTool.Misc;
using ExcelDataReader;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Text;

namespace DeploymentTool.Model
{
    public class ProjectFieldProcessor
    {
        private dtDBEntities db = new dtDBEntities();
        public ProjectFieldProcessor() { }

        public Dictionary<string, IProjectExcelFields> ImportFields(string strFilePath, ProjectType projectType, out string strStoreNumbers)
        {
            strStoreNumbers = string.Empty;
            //  
            Dictionary<string, IProjectExcelFields> fields = new Dictionary<string, IProjectExcelFields>();
            try
            {
                TraceUtility.WriteTrace("AttachmentController", "Starting ImportExceltoDatabase");
                IProjectExcelFields objProjectExcel = null;
                DataTable dt = new DataTable();
                try
                {
                    DataTable dtNew = new DataTable();
                    using (var stream = File.Open(strFilePath, FileMode.Open, FileAccess.Read))
                    {
                        using (var reader = ExcelReaderFactory.CreateReader(stream))
                        {
                            List<string> lstStoreNumber = new List<string>();

                            // 1. Use the reader methods
                            do
                            {
                                reader.Read();
                                int ColumnCount = reader.FieldCount;
                                for (int i = 0; i < ColumnCount; i++)
                                {
                                    string ColumnName = reader.GetValue(i).ToString();
                                    if (!dtNew.Columns.Contains(ColumnName))
                                    { dtNew.Columns.Add(ColumnName); }
                                }
                                while (reader.Read())
                                {
                                    if (reader.FieldCount == 0)
                                        break;
                                    switch (projectType)
                                    {
                                        case ProjectType.ArbysHPRollout:
                                            objProjectExcel = new ProjectExcelFieldsHPRollout().getModelFromColumns(reader, dtNew);
                                            break;
                                        case ProjectType.OrderAccuracy:
                                            objProjectExcel = new ProjectExcelFieldsOrderAccurcy().getModelFromColumns(reader, dtNew);
                                            break;
                                        case ProjectType.OrderStatusBoard:
                                            objProjectExcel = new ProjectExcelFieldsOrderStatusBoard().getModelFromColumns(reader, dtNew);
                                            break;
                                        case ProjectType.ServerHandheld:
                                            objProjectExcel = new ProjectExcelFieldsServerHandheld().getModelFromColumns(reader, dtNew);
                                            break;
                                    }
                                    string tStoreNumber = objProjectExcel.getStoreNumber();
                                    if (objProjectExcel != null && !string.IsNullOrEmpty(tStoreNumber))
                                    {
                                        lstStoreNumber.Add(objProjectExcel.getStoreNumber());

                                        if (!fields.ContainsKey(objProjectExcel.getStoreNumber()))
                                            fields.Add(objProjectExcel.getStoreNumber(), objProjectExcel);
                                        else
                                            fields[objProjectExcel.getStoreNumber()] = objProjectExcel;
                                    }
                                    else
                                        break;
                                }
                            } while (reader.NextResult());
                            strStoreNumbers = string.Join(",", lstStoreNumber.ToArray());
                        }
                    }

                }
                catch (Exception ex)
                {
                    TraceUtility.ForceWriteException("ImportExceltoDatabase", HttpContext.Current, ex);
                    //result = false;
                }
                finally
                {
                    //oledbConn.Close();
                }
            }
            catch (Exception ex)
            {
                TraceUtility.ForceWriteException("ImportExceltoDatabase2", HttpContext.Current, ex);
            }
            // return ip;
            // return result;
            return fields;
        }

        public List<IProjectExcelFields> UpdateStoreExistFields(Dictionary<string, IProjectExcelFields> itemsByStoreNumber, int nProjectType, string storeNumbers, int nBrandId, int nRolloutId)
        {
            SqlParameter tModuleNameParam1 = new SqlParameter("@nBrandId", nBrandId);
            SqlParameter tModuleNameParam2 = new SqlParameter("@tStoreNumber", storeNumbers);
            SqlParameter tModuleNameParam3 = new SqlParameter("@nRolloutId", nRolloutId);
            SqlParameter tModuleNameParam4 = new SqlParameter("@nProjectType", nProjectType);
            var output = db.Database.SqlQuery<StoreExistModel>("exec sproc_StoreExistCheckForRollout @nBrandId, @tStoreNumber, @nRolloutId, @nProjectType",
                tModuleNameParam1, tModuleNameParam2, tModuleNameParam3, tModuleNameParam4).ToList();

            foreach (var tItem in output)
            {
                if (itemsByStoreNumber.ContainsKey(tItem.StoreNumber))
                {
                    itemsByStoreNumber[tItem.StoreNumber].setValues(tItem.ExistFlag, tItem.ProjectId, tItem.StoreId);
                }
            }

            return itemsByStoreNumber.Values.ToList();
        }
    }

    public class StoreExistModel
    {
        public string StoreNumber { get; set; }
        public Nullable<int> StoreId { get; set; }
        public Nullable<int> ProjectId { get; set; }
        public int ExistFlag { get; set; }
    }
}