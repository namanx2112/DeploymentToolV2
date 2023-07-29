using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace DeploymentTool.Misc
{
    public class TraceUtility
    {
        private static dtDBEntities db = new dtDBEntities();
        static bool _traceEnable = false;
        static object _lock = new object();
        public static void EnableDisable(bool enable)
        {
            lock (_lock)
            {
                _traceEnable = enable;
            }
        }

        public static async void WriteTrace(string sApplication, string sTrace, params string[] items)
        {
            Task.Run(() => LogTrace(sApplication, sTrace, items));
        }

        static async void LogTrace(string sApplication, string sTrace, params string[] items)
        {
            try
            {
                if (_traceEnable)
                {
                    string sCompleteTrace = string.Format(sTrace, items);
                    tblApplicationTrace traec = new tblApplicationTrace();
                    traec.tModule = sApplication;
                    traec.tTrace = sCompleteTrace;
                    db.tblApplicationTraces.Add(traec);
                    db.SaveChanges();
                }
            }
            catch (Exception e)
            {

            }
        }
    }

    public enum TraceLevel
    {
        Info, Warn, Error
    }
}