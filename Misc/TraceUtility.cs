using System;
using System.Collections.Generic;
using System.Linq;
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

        public static void WriteTrace(string sApplication, string sTrace)
        {
            try
            {
                if (_traceEnable)
                {
                    tblApplicationTrace traec = new tblApplicationTrace();
                    traec.tModule = sApplication;
                    traec.tTrace = sTrace;
                    db.tblApplicationTraces.Add(traec);
                    db.SaveChanges();
                }
            }
            catch (Exception e)
            {

            }
        }
    }
}