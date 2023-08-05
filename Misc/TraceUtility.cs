using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Services.Description;

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
            Task.Run(() => LogTrace(_traceEnable, sApplication, sTrace, items));
        }

        public static async void ForceWriteException(string sApplication, HttpContext context, System.Exception ex)
        {
            string sContext = string.Format("Request URL:{0}, Referrer:{1}", context.Request.Url.ToString(), context.Request.UrlReferrer.AbsolutePath);
            string strMessage = string.Format("Exception Occured Message:{0}, InnerException:{1}, sContext{2}", ex.Message, (ex.InnerException != null) ? ex.InnerException.Message : "", sContext);
            ForceWriteException(sApplication, strMessage);
        }

        public static async void ForceWriteException(string sApplication, string sTrace, params string[] items)
        {
            Task.Run(() => LogTrace(true, sApplication, sTrace, items));
        }

        static async void LogTrace(Boolean canWrite, string sApplication, string sTrace, params string[] items)
        {
            try
            {
                if (canWrite)
                {
                    string sCompleteTrace = string.Format(sTrace, items);
                    tblApplicationTrace traec = new tblApplicationTrace();
                    traec.dTraceTime = DateTime.Now;
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