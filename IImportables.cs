using ExcelDataReader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeploymentTool
{
    public interface IImportables
    {
        List<IImportModel> Import(string strPath, int nBrandId, int nInstanceId);
    }

    public interface IImportModel
    {
        IImportModel GetFromExcel(IExcelDataReader reader);
    }
}
