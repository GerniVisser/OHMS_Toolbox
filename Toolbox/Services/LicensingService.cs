using QlmLicenseLib;
using System;

namespace Toolbox.Services
{
    class LicensingService
    {
        static QLM.LicenseValidator lv;
        static string settingsFile;
        static string wizardExec;

        public static void ValidateLicense()
        {
            System.Reflection.Assembly thisAssembly = System.Reflection.Assembly.GetExecutingAssembly();
            string location = System.IO.Path.GetDirectoryName(thisAssembly.Location);

            settingsFile = System.IO.Path.Combine(location, "OHMS Toolbox 1.0.lw.xml");

            //
            // This sample uses the QlmLicenseWizard.exe which is not currently cross platform (requires .NET 4).
            // For mac and linux, you will need to create your own user interface for activation
            //
            wizardExec = System.IO.Path.Combine(location, "QlmLicenseWizard.exe");

            lv = new QLM.LicenseValidator(settingsFile);

            bool needsActivation = false;
            string errorMsg = string.Empty;

            ELicenseBinding licenseBinding = ELicenseBinding.ComputerName;

            if (lv.ValidateLicenseAtStartup(licenseBinding, ref needsActivation, ref errorMsg) == false)
            {
                int exitCode = DisplayLicenseForm();

                if (exitCode == 4)
                {
                    Environment.Exit(0);
                }

                if (lv.ValidateLicenseAtStartup(licenseBinding, ref needsActivation, ref errorMsg) == false)
                {
                    Environment.Exit(0);
                }
            }

            Console.WriteLine(errorMsg);

        }
        static private int DisplayLicenseForm()
        {
            string errorMessage;
            if (lv.QlmLicenseObject.ValidateSettingsFile(settingsFile, out errorMessage) == false)
            {
                Console.WriteLine(errorMessage);
                return 0;
            }

            string args = String.Format("/settings \"{0}\"", settingsFile);

            if (!System.IO.File.Exists(wizardExec))
            {
                wizardExec = @"C:\Program Files\Soraco\QuickLicenseMgr\QlmLicenseWizard.exe";
            }

            return lv.QlmLicenseObject.LaunchProcess(wizardExec, args, true, true);
        }
    }
}

