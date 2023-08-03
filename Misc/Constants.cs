using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DeploymentTool.Misc
{
    public class Constants
    {
    }

    public enum ProjectType
    {
        New, Rebuild, Remodel, Relocation, Acquisition, POSInstallation, AudioInstallation, MenuInstallation, PaymentTerminalInstallation, PartsReplacement
    }

    public enum Permission
    {
        None, View, Edit
    }
}