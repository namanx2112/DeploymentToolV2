using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DeploymentTool.Misc
{
    public class TraceUtility
    {
        private static dtDBEntities db = new dtDBEntities();

        public static void WriteTrace(string sApplication, string sTrace)
        {
            try
            {
                tblApplicationTrace traec = new tblApplicationTrace();
                traec.tModule= sApplication;
                traec.tTrace= sTrace;
                db.tblApplicationTraces.Add(traec);
                db.SaveChanges();
            }
            catch(Exception e)
            {

            }
        }
    }
}