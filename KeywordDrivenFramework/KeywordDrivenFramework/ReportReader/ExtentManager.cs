using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using AventStack.ExtentReports.Reporter.Configuration;
using KeywordDrivenFramework.CommonUtilities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeywordDrivenFramework.ReportReader
{
    public class ExtentManager
    {
        private static readonly Lazy<ExtentReports> _lazy = new Lazy<ExtentReports>(() => new ExtentReports());
        public static ExtentReports Instance { get { return _lazy.Value; } }
        static ExtentManager()
        {
            string OS = ConfigurationManager.AppSettings["OS"].ToString();
            string HostName = ConfigurationManager.AppSettings["HostName"].ToString();
            string Environment = ConfigurationManager.AppSettings["Environment"].ToString();
            string UserName = ConfigurationManager.AppSettings["UserName"].ToString();
            DirectoryInfo dirScreenshotPath = new DirectoryInfo(GeneralMethod.GetScreenshotPath());
            foreach (FileInfo fi in dirScreenshotPath.GetFiles())
            {
                fi.Delete();
            }
            var htmlReporter = new ExtentHtmlReporter(GeneralMethod.GetReportPath());
            htmlReporter.Configuration().ChartLocation = ChartLocation.Bottom;
            htmlReporter.Configuration().ChartVisibilityOnOpen = true;
            htmlReporter.Configuration().DocumentTitle = "Extent/NUnit Samples";
            htmlReporter.Configuration().ReportName = "Extent/NUnit Samples";
            htmlReporter.Configuration().Theme = Theme.Standard;
            Instance.AttachReporter(htmlReporter);
            Instance.AddSystemInfo("os", OS);
            Instance.AddSystemInfo("Host Name", HostName);
            Instance.AddSystemInfo("Environment", Environment);
            Instance.AddSystemInfo("User Name", UserName);
        }
        private ExtentManager()
        {
        }
    }
}
