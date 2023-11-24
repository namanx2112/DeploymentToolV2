using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DeploymentTool.Misc
{
    public class Constants
    {
        public static string CCEmailAddress = (System.Configuration.ConfigurationManager.AppSettings["CCEmailAddress"] != null) ? System.Configuration.ConfigurationManager.AppSettings["CCEmailAddress"] : string.Empty;
    }

    public enum ProjectType
    {
        New, Rebuild, Remodel, Relocation, Acquisition, POSInstallation, AudioInstallation, InteriorMenuInstallation, PaymentTerminalInstallation, PartsReplacement, ServerHandheld, ExteriorMenuInstallation,
        OrderAccuracy, OrderStatusBoard, ArbysHPRollout
    }

    public enum Permission
    {
        None, View, Edit
    }
}