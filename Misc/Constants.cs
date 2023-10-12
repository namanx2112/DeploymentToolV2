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
        New, Rebuild, Remodel, Relocation, Acquisition, POSInstallation, AudioInstallation, InteriorMenuInstallation, PaymentTerminalInstallation, PartsReplacement, ServerHandheld, ExteriorMenuInstallation
    }

    public enum Permission
    {
        None, View, Edit
    }
}