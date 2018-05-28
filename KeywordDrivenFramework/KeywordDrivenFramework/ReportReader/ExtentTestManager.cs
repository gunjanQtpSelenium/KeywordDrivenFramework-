using AventStack.ExtentReports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace KeywordDrivenFramework.ReportReader
{
    public static class ExtentTestManager
    {
        [ThreadStatic]
        public static ExtentTest _parentTest;

        public static ExtentTest _childTest { get; set; }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public static ExtentTest CreateParentTest(string testName, string description = null)
        {
            _parentTest = ExtentManager.Instance.CreateTest(testName, description);
            return _parentTest;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public static ExtentTest CreateTest(string testName, string description = null)
        {
            _childTest = _parentTest.CreateNode(testName, description);
            return _childTest;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public static ExtentTest CreateLog(string logMessage)
        {
            _childTest = _childTest.Log(Status.Info, logMessage);
            return _childTest;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public static ExtentTest GetTest()
        {
            return _childTest;
        }
    }
}
